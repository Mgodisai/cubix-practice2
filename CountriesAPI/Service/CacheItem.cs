namespace CountriesAPI.Service
{
   internal class CacheItem<T>
   {
      public T Data { get; set; }
      public DateTime Timestamp { get; set; }
   }
}
