﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Checkers.CheckersServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameStatus", Namespace="http://schemas.datacontract.org/2004/07/CheckersAppServer")]
    [System.SerializableAttribute()]
    public partial struct GameStatus : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BlackScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ExistsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Checkers.CheckersServiceReference.LogItem[] LogField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Checkers.CheckersServiceReference.Player PlayerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlayerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int WhiteScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Checkers.CheckersServiceReference.WinPlayer WinPlayerField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int BlackScore {
            get {
                return this.BlackScoreField;
            }
            set {
                if ((this.BlackScoreField.Equals(value) != true)) {
                    this.BlackScoreField = value;
                    this.RaisePropertyChanged("BlackScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Exists {
            get {
                return this.ExistsField;
            }
            set {
                if ((this.ExistsField.Equals(value) != true)) {
                    this.ExistsField = value;
                    this.RaisePropertyChanged("Exists");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Checkers.CheckersServiceReference.LogItem[] Log {
            get {
                return this.LogField;
            }
            set {
                if ((object.ReferenceEquals(this.LogField, value) != true)) {
                    this.LogField = value;
                    this.RaisePropertyChanged("Log");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Checkers.CheckersServiceReference.Player Player {
            get {
                return this.PlayerField;
            }
            set {
                if ((this.PlayerField.Equals(value) != true)) {
                    this.PlayerField = value;
                    this.RaisePropertyChanged("Player");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlayerName {
            get {
                return this.PlayerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PlayerNameField, value) != true)) {
                    this.PlayerNameField = value;
                    this.RaisePropertyChanged("PlayerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int WhiteScore {
            get {
                return this.WhiteScoreField;
            }
            set {
                if ((this.WhiteScoreField.Equals(value) != true)) {
                    this.WhiteScoreField = value;
                    this.RaisePropertyChanged("WhiteScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Checkers.CheckersServiceReference.WinPlayer WinPlayer {
            get {
                return this.WinPlayerField;
            }
            set {
                if ((this.WinPlayerField.Equals(value) != true)) {
                    this.WinPlayerField = value;
                    this.RaisePropertyChanged("WinPlayer");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LogItem", Namespace="http://schemas.datacontract.org/2004/07/Checkers")]
    [System.SerializableAttribute()]
    public partial class LogItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BlackField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WhiteField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Black {
            get {
                return this.BlackField;
            }
            set {
                if ((object.ReferenceEquals(this.BlackField, value) != true)) {
                    this.BlackField = value;
                    this.RaisePropertyChanged("Black");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Number {
            get {
                return this.NumberField;
            }
            set {
                if ((this.NumberField.Equals(value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string White {
            get {
                return this.WhiteField;
            }
            set {
                if ((object.ReferenceEquals(this.WhiteField, value) != true)) {
                    this.WhiteField = value;
                    this.RaisePropertyChanged("White");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/CheckersAppServer")]
    public enum Player : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        White = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Black = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WinPlayer", Namespace="http://schemas.datacontract.org/2004/07/CheckersAppServer")]
    public enum WinPlayer : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Game = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        White = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Black = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Draw = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlayMode", Namespace="http://schemas.datacontract.org/2004/07/CheckersAppServer")]
    public enum PlayMode : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Game = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NetGame = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TestGame = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Collocation = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CheckersServiceReference.ICheckersService", CallbackContract=typeof(Checkers.CheckersServiceReference.ICheckersServiceCallback))]
    public interface ICheckersService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetUserPasswordHash", ReplyAction="http://tempuri.org/ICheckersService/GetUserPasswordHashResponse")]
        string GetUserPasswordHash(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetUserPasswordHash", ReplyAction="http://tempuri.org/ICheckersService/GetUserPasswordHashResponse")]
        System.Threading.Tasks.Task<string> GetUserPasswordHashAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetUserNames", ReplyAction="http://tempuri.org/ICheckersService/GetUserNamesResponse")]
        string[] GetUserNames();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetUserNames", ReplyAction="http://tempuri.org/ICheckersService/GetUserNamesResponse")]
        System.Threading.Tasks.Task<string[]> GetUserNamesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetUserProperties", ReplyAction="http://tempuri.org/ICheckersService/GetUserPropertiesResponse")]
        string[] GetUserProperties(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetUserProperties", ReplyAction="http://tempuri.org/ICheckersService/GetUserPropertiesResponse")]
        System.Threading.Tasks.Task<string[]> GetUserPropertiesAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/AddUser", ReplyAction="http://tempuri.org/ICheckersService/AddUserResponse")]
        bool AddUser(string fullname, string position, string sector, string hash);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/AddUser", ReplyAction="http://tempuri.org/ICheckersService/AddUserResponse")]
        System.Threading.Tasks.Task<bool> AddUserAsync(string fullname, string position, string sector, string hash);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/ChangeUser", ReplyAction="http://tempuri.org/ICheckersService/ChangeUserResponse")]
        bool ChangeUser(string id, string fullname, string position, string sector, string hash);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/ChangeUser", ReplyAction="http://tempuri.org/ICheckersService/ChangeUserResponse")]
        System.Threading.Tasks.Task<bool> ChangeUserAsync(string id, string fullname, string position, string sector, string hash);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/DeleteUser", ReplyAction="http://tempuri.org/ICheckersService/DeleteUserResponse")]
        bool DeleteUser(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/DeleteUser", ReplyAction="http://tempuri.org/ICheckersService/DeleteUserResponse")]
        System.Threading.Tasks.Task<bool> DeleteUserAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/CreateGame", ReplyAction="http://tempuri.org/ICheckersService/CreateGameResponse")]
        System.Guid CreateGame();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/CreateGame", ReplyAction="http://tempuri.org/ICheckersService/CreateGameResponse")]
        System.Threading.Tasks.Task<System.Guid> CreateGameAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/DestroyGame", ReplyAction="http://tempuri.org/ICheckersService/DestroyGameResponse")]
        bool DestroyGame(System.Guid gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/DestroyGame", ReplyAction="http://tempuri.org/ICheckersService/DestroyGameResponse")]
        System.Threading.Tasks.Task<bool> DestroyGameAsync(System.Guid gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetActiveGames", ReplyAction="http://tempuri.org/ICheckersService/GetActiveGamesResponse")]
        System.Guid[] GetActiveGames();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetActiveGames", ReplyAction="http://tempuri.org/ICheckersService/GetActiveGamesResponse")]
        System.Threading.Tasks.Task<System.Guid[]> GetActiveGamesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetGameStatus", ReplyAction="http://tempuri.org/ICheckersService/GetGameStatusResponse")]
        Checkers.CheckersServiceReference.GameStatus GetGameStatus(System.Guid gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetGameStatus", ReplyAction="http://tempuri.org/ICheckersService/GetGameStatusResponse")]
        System.Threading.Tasks.Task<Checkers.CheckersServiceReference.GameStatus> GetGameStatusAsync(System.Guid gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/StartNewGame", ReplyAction="http://tempuri.org/ICheckersService/StartNewGameResponse")]
        bool StartNewGame(System.Guid gameId, Checkers.CheckersServiceReference.PlayMode gameType, Checkers.CheckersServiceReference.Player player, string playerName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/StartNewGame", ReplyAction="http://tempuri.org/ICheckersService/StartNewGameResponse")]
        System.Threading.Tasks.Task<bool> StartNewGameAsync(System.Guid gameId, Checkers.CheckersServiceReference.PlayMode gameType, Checkers.CheckersServiceReference.Player player, string playerName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/EndGame", ReplyAction="http://tempuri.org/ICheckersService/EndGameResponse")]
        bool EndGame(System.Guid gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/EndGame", ReplyAction="http://tempuri.org/ICheckersService/EndGameResponse")]
        System.Threading.Tasks.Task<bool> EndGameAsync(System.Guid gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetDrawBoardScript", ReplyAction="http://tempuri.org/ICheckersService/GetDrawBoardScriptResponse")]
        string GetDrawBoardScript(System.Guid gameId, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetDrawBoardScript", ReplyAction="http://tempuri.org/ICheckersService/GetDrawBoardScriptResponse")]
        System.Threading.Tasks.Task<string> GetDrawBoardScriptAsync(System.Guid gameId, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/OnBoardMouseDown", ReplyAction="http://tempuri.org/ICheckersService/OnBoardMouseDownResponse")]
        bool OnBoardMouseDown(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/OnBoardMouseDown", ReplyAction="http://tempuri.org/ICheckersService/OnBoardMouseDownResponse")]
        System.Threading.Tasks.Task<bool> OnBoardMouseDownAsync(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/OnBoardMouseMove", ReplyAction="http://tempuri.org/ICheckersService/OnBoardMouseMoveResponse")]
        bool OnBoardMouseMove(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/OnBoardMouseMove", ReplyAction="http://tempuri.org/ICheckersService/OnBoardMouseMoveResponse")]
        System.Threading.Tasks.Task<bool> OnBoardMouseMoveAsync(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/OnBoardMouseUp", ReplyAction="http://tempuri.org/ICheckersService/OnBoardMouseUpResponse")]
        bool OnBoardMouseUp(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/OnBoardMouseUp", ReplyAction="http://tempuri.org/ICheckersService/OnBoardMouseUpResponse")]
        System.Threading.Tasks.Task<bool> OnBoardMouseUpAsync(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/RegisterForUpdates", ReplyAction="http://tempuri.org/ICheckersService/RegisterForUpdatesResponse")]
        bool RegisterForUpdates(System.Guid clientId, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/RegisterForUpdates", ReplyAction="http://tempuri.org/ICheckersService/RegisterForUpdatesResponse")]
        System.Threading.Tasks.Task<bool> RegisterForUpdatesAsync(System.Guid clientId, Checkers.CheckersServiceReference.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICheckersService/UpdateGame")]
        void UpdateGame(System.Guid clientId, System.Guid gameId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICheckersService/UpdateGame")]
        System.Threading.Tasks.Task UpdateGameAsync(System.Guid clientId, System.Guid gameId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICheckersService/Disconnect")]
        void Disconnect(System.Guid clientId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICheckersService/Disconnect")]
        System.Threading.Tasks.Task DisconnectAsync(System.Guid clientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetDate", ReplyAction="http://tempuri.org/ICheckersService/GetDateResponse")]
        System.DateTime GetDate();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICheckersService/GetDate", ReplyAction="http://tempuri.org/ICheckersService/GetDateResponse")]
        System.Threading.Tasks.Task<System.DateTime> GetDateAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICheckersServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICheckersService/GameUpdated")]
        void GameUpdated(Checkers.CheckersServiceReference.GameStatus gameStatus);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICheckersServiceChannel : Checkers.CheckersServiceReference.ICheckersService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CheckersServiceClient : System.ServiceModel.DuplexClientBase<Checkers.CheckersServiceReference.ICheckersService>, Checkers.CheckersServiceReference.ICheckersService {
        
        public CheckersServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public CheckersServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public CheckersServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CheckersServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CheckersServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string GetUserPasswordHash(string id) {
            return base.Channel.GetUserPasswordHash(id);
        }
        
        public System.Threading.Tasks.Task<string> GetUserPasswordHashAsync(string id) {
            return base.Channel.GetUserPasswordHashAsync(id);
        }
        
        public string[] GetUserNames() {
            return base.Channel.GetUserNames();
        }
        
        public System.Threading.Tasks.Task<string[]> GetUserNamesAsync() {
            return base.Channel.GetUserNamesAsync();
        }
        
        public string[] GetUserProperties(string id) {
            return base.Channel.GetUserProperties(id);
        }
        
        public System.Threading.Tasks.Task<string[]> GetUserPropertiesAsync(string id) {
            return base.Channel.GetUserPropertiesAsync(id);
        }
        
        public bool AddUser(string fullname, string position, string sector, string hash) {
            return base.Channel.AddUser(fullname, position, sector, hash);
        }
        
        public System.Threading.Tasks.Task<bool> AddUserAsync(string fullname, string position, string sector, string hash) {
            return base.Channel.AddUserAsync(fullname, position, sector, hash);
        }
        
        public bool ChangeUser(string id, string fullname, string position, string sector, string hash) {
            return base.Channel.ChangeUser(id, fullname, position, sector, hash);
        }
        
        public System.Threading.Tasks.Task<bool> ChangeUserAsync(string id, string fullname, string position, string sector, string hash) {
            return base.Channel.ChangeUserAsync(id, fullname, position, sector, hash);
        }
        
        public bool DeleteUser(string id) {
            return base.Channel.DeleteUser(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteUserAsync(string id) {
            return base.Channel.DeleteUserAsync(id);
        }
        
        public System.Guid CreateGame() {
            return base.Channel.CreateGame();
        }
        
        public System.Threading.Tasks.Task<System.Guid> CreateGameAsync() {
            return base.Channel.CreateGameAsync();
        }
        
        public bool DestroyGame(System.Guid gameId) {
            return base.Channel.DestroyGame(gameId);
        }
        
        public System.Threading.Tasks.Task<bool> DestroyGameAsync(System.Guid gameId) {
            return base.Channel.DestroyGameAsync(gameId);
        }
        
        public System.Guid[] GetActiveGames() {
            return base.Channel.GetActiveGames();
        }
        
        public System.Threading.Tasks.Task<System.Guid[]> GetActiveGamesAsync() {
            return base.Channel.GetActiveGamesAsync();
        }
        
        public Checkers.CheckersServiceReference.GameStatus GetGameStatus(System.Guid gameId) {
            return base.Channel.GetGameStatus(gameId);
        }
        
        public System.Threading.Tasks.Task<Checkers.CheckersServiceReference.GameStatus> GetGameStatusAsync(System.Guid gameId) {
            return base.Channel.GetGameStatusAsync(gameId);
        }
        
        public bool StartNewGame(System.Guid gameId, Checkers.CheckersServiceReference.PlayMode gameType, Checkers.CheckersServiceReference.Player player, string playerName) {
            return base.Channel.StartNewGame(gameId, gameType, player, playerName);
        }
        
        public System.Threading.Tasks.Task<bool> StartNewGameAsync(System.Guid gameId, Checkers.CheckersServiceReference.PlayMode gameType, Checkers.CheckersServiceReference.Player player, string playerName) {
            return base.Channel.StartNewGameAsync(gameId, gameType, player, playerName);
        }
        
        public bool EndGame(System.Guid gameId) {
            return base.Channel.EndGame(gameId);
        }
        
        public System.Threading.Tasks.Task<bool> EndGameAsync(System.Guid gameId) {
            return base.Channel.EndGameAsync(gameId);
        }
        
        public string GetDrawBoardScript(System.Guid gameId, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.GetDrawBoardScript(gameId, player);
        }
        
        public System.Threading.Tasks.Task<string> GetDrawBoardScriptAsync(System.Guid gameId, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.GetDrawBoardScriptAsync(gameId, player);
        }
        
        public bool OnBoardMouseDown(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.OnBoardMouseDown(gameId, location, modifierKeys, player);
        }
        
        public System.Threading.Tasks.Task<bool> OnBoardMouseDownAsync(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.OnBoardMouseDownAsync(gameId, location, modifierKeys, player);
        }
        
        public bool OnBoardMouseMove(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.OnBoardMouseMove(gameId, location, modifierKeys, player);
        }
        
        public System.Threading.Tasks.Task<bool> OnBoardMouseMoveAsync(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.OnBoardMouseMoveAsync(gameId, location, modifierKeys, player);
        }
        
        public bool OnBoardMouseUp(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.OnBoardMouseUp(gameId, location, modifierKeys, player);
        }
        
        public System.Threading.Tasks.Task<bool> OnBoardMouseUpAsync(System.Guid gameId, System.Drawing.Point location, int modifierKeys, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.OnBoardMouseUpAsync(gameId, location, modifierKeys, player);
        }
        
        public bool RegisterForUpdates(System.Guid clientId, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.RegisterForUpdates(clientId, player);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterForUpdatesAsync(System.Guid clientId, Checkers.CheckersServiceReference.Player player) {
            return base.Channel.RegisterForUpdatesAsync(clientId, player);
        }
        
        public void UpdateGame(System.Guid clientId, System.Guid gameId) {
            base.Channel.UpdateGame(clientId, gameId);
        }
        
        public System.Threading.Tasks.Task UpdateGameAsync(System.Guid clientId, System.Guid gameId) {
            return base.Channel.UpdateGameAsync(clientId, gameId);
        }
        
        public void Disconnect(System.Guid clientId) {
            base.Channel.Disconnect(clientId);
        }
        
        public System.Threading.Tasks.Task DisconnectAsync(System.Guid clientId) {
            return base.Channel.DisconnectAsync(clientId);
        }
        
        public System.DateTime GetDate() {
            return base.Channel.GetDate();
        }
        
        public System.Threading.Tasks.Task<System.DateTime> GetDateAsync() {
            return base.Channel.GetDateAsync();
        }
    }
}
