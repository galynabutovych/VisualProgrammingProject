namespace Snake
{
    public class StorageManager
    {
        static Storage storageInstance = new XmlStorage();
        public Storage StorageInstance
        {
            get {return storageInstance;}
        }
    }    
}