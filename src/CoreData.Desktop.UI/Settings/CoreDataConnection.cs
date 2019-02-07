//using CoreData.Desktop.Server.Settings;
//using Polly;
//using System;
//using System.Threading.Tasks;

//namespace CoreData.Desktop.UI.Settings
//{
//    public class CoreDataConnection : ICoreDataConnection
//    {
//        //private readonly Func<IClient> _connection;
//        //private readonly Func<IFileSystem> _fileSystem;

//        //public CoreDataConnection(
//        //    Func<IClient> connection,
//        //    Func<IFileSystem> fileSystem)
//        //{
//        //    _connection = connection;
//        //    _fileSystem = fileSystem;
//        //}

//        //public IConnection CoreData { get; private set; }
//        //public IFileSystem FileSystem { get; private set; }
//        //public ICoreDataFileSystem CurrentFileSystem { get; private set; }

//        public async Task<bool> Connect(string host, string user, string pwd, bool silent)
//        {
//            var coreData = new Uri(host);
//            var connection = new ConnectionInfo(coreData);
//            var client = new Server.RestClient(connection); //, user, pwd);

//            var authenticated = await Policy
//                .Handle<Exception>()
//                .WaitAndRetryAsync(
//                new[] 
//                {
//                    TimeSpan.FromSeconds(1),
//                    TimeSpan.FromSeconds(2)
//                })
//                .ExecuteAsync(() => client.Authenticate());

//            //if(authenticated)
//            //{
//            //    var driveSettings = new VirtualDriveSettings('S', true);
//            //    CurrentFileSystem = new VirtualDrive(driveSettings, client).Mount();
//            //}

//            return authenticated;
//        }
//    }

//    public interface ICoreDataConnection
//    {
//        //IConnection CoreData { get; }
//        //IFileSystem FileSystem { get; }

//        Task<bool> Connect(string host, string user, string pwd, bool silent);
//    }
//}
