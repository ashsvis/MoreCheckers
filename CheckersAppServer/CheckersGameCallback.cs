﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace CheckersAppServer
{
    public partial class CheckersService : ICheckersService
    {
        private class CallbackInfo
        {
            public IClientCallback ClientCallback;
            public Guid ClientId;
        }

        private class Worker
        {
            public readonly List<CallbackInfo> Callbacks = new List<CallbackInfo>();
            public Guid GameId { get; set; }
        }

        private static readonly Hashtable Workers = new Hashtable();


        public void RegisterForUpdates(Guid clientId)
        {
            new Thread(current =>
            {
                lock (Workers.SyncRoot)
                {
                    // при необходимости создаем новый рабочий объект, добавляем его
                    // добавляем его в хэш-таблицу и запускаем в отдельном потоке
                    Worker worker = new Worker { GameId = clientId };
                    Workers[clientId] = worker;
                    // Получить рабочий объект для данной category и добавить
                    // прокси клиента в список обратных вызовов
                    var callback = ((OperationContext)current).GetCallbackChannel<IClientCallback>();
                    lock (worker.Callbacks)
                    {
                        worker.Callbacks.Add(new CallbackInfo
                        {
                            ClientCallback = callback,
                            ClientId = clientId
                        });
                    }
                }
            }).Start(OperationContext.Current);
        }

        public void Disconnect(Guid clientId)
        {
            ThreadPool.QueueUserWorkItem(param =>
            {
                lock (Workers.SyncRoot)
                {
                    foreach (var callbacks in from DictionaryEntry worker in Workers
                                              select ((Worker)worker.Value).Callbacks)
                    {
                        lock (callbacks)
                        {
                            var removing =
                                callbacks.Where(cbinfo => cbinfo.ClientId.Equals(clientId)).ToList();
                            foreach (var callback in removing) callbacks.Remove(callback);
                        }
                    }
                }
            });
        }

        public void UpdateGame(Guid clientId)
        {
            CustomUpdateGame(clientId, false);
        }

        private static void CustomUpdateGame(Guid clientId, bool self)
        {
            ThreadPool.QueueUserWorkItem(param =>
            {
                lock (Workers.SyncRoot)
                {
                    foreach (var callbacks in from DictionaryEntry worker in Workers
                                              where !((Worker)worker.Value).GameId.Equals(clientId)
                                              select ((Worker)worker.Value).Callbacks)
                    {
                        lock (callbacks)
                        {
                            var removing = new List<CallbackInfo>();
                            foreach (var callbackInfo in callbacks)
                            {
                                try
                                {
                                    callbackInfo.ClientCallback.GameUpdated(clientId);
                                }
                                catch (CommunicationObjectAbortedException)
                                {
                                    removing.Add(callbackInfo);
                                }
                                catch (CommunicationException ex)
                                {
                                    Console.WriteLine("Ошибка при отправке кешированного значения клиенту: " + ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Ошибка в CustomUpdateProperty() класса CheckersService: " + ex.Message);
                                }
                            }
                            foreach (var callbackInfo in removing) callbacks.Remove(callbackInfo);
                        }
                    }
                }
            });
        }

    }
}
