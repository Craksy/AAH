namespace AutoAuctionProjekt; 

public interface IDatabaseAdapter<T> {
    // construct a new object from a DataReader
    T Construct(System.Data.IDataReader reader);
    
    void Create(T obj);
    T Read(uint id);
    void Update(T obj);
    void Delete(uint id);
}