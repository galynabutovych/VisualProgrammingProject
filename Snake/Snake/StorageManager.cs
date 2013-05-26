namespace Snake
{
    public class StorageManager
    {
        public static Storage storageInstance = new XmlStorage();
        public Storage StorageInstance
        {
            get {return storageInstance;}
        }
    }    
}