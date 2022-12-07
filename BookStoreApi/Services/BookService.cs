﻿namespace BookStoreApi.Services
{
    using Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using System.Formats.Asn1;

    public class BookService
    {
        private readonly IMongoCollection<Book> _bookCollection;
        public BookService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);

            _bookCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BookCollectionName);

        }
        public async Task<List<Book>> GetAsync() =>
            await _bookCollection.Find(_ => true).ToListAsync();

        public async Task<Book> GetAsync(string id) => 
            await _bookCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Book newBook) => 
            await _bookCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Book updatedBook) =>
            await _bookCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _bookCollection.DeleteOneAsync(x => x.Id == id);
                
    }
}
