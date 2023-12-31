# Assignment

Create production-ready ASP.NET Core service app that provides API for storage and
retrive documents in different formats
1. The documents are send as a payload of POST request in JSON format and could be
modified via PUT verb.
2. The service is able to return the stored documents in different format, such as XML,
MessagePack, etc.
3. It must be easy to add support for new formats.
4. It must be easy to add different underlying storage, like cloud, HDD, InMemory, etc.
5. Assume that the load of this service will be very high (mostly reading).
6. Demonstrate ability to write unit tests.
7. The document has mandatory field id, tags and data as shown bellow. The schema
of the data field can be arbitrary.


```POST http://localhost:5000/documents
Content-Type: application/json; charset=UTF-8
{
    "id": "some-unique-identifier1",
    "tags": ["important", ".net"]
    "data": {
        "some": "data",
        "optional": "fields"
    }
}

GET http://localhost:5000/documents/some-unique-identifier1
Accept: application/xml
```

# Solution
1. Created three endpoints
```
GetDocument(int id)
CreateDocument(DocumentModifyDTO doc)
UpdateDocument(int id, DocumentModifyDTO doc)
```
2. Solved by a built-in ASP.NET middleware in Program.cs class
```
options.OutputFormatters.Add(new MessagePackOutputFormatter());
options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
```

3. It is provided by a build-in serializer, therefore it is quite easy to add new formats.

4. The possibility to easily change a storage is assured by providing an interface for repository.
```
public interface IDocumentRepository
{
    Task<Document> CreateDocumentAsync(DocumentModifyDTO document);
    Task<Document> GetDocumentAsync(int id);
    Task<Document> UpdateDocumentAsync(int documentId, DocumentModifyDTO document);
}
```
We can implement multiple repositories implementing this interface. E.g. we can create an implementation using `dbContext` to access the database
5. The load optimization is assured by using asynchronous approach and also using caching in middleware, in Program.cs
```
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
    builder.Expire(TimeSpan.FromSeconds(25)));
});
```
6. There are unit tests provided in the testing project

7. I have created an entity according to requirements, The content of `data` in the body in the assignment indicates that there is always key-value pair therefore I have used a `Dictionary` data type.  
However, if my assumptions are wrong, I would go for `dynamic` data type
```
public class Document
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string[] Tags { get; set; }
    
    [Required]
    public Dictionary<string, string> Data { get; set; }
}
```

After a run , there is a Swagger page available `https://localhost:7216/swagger/index.html`

This solution is quite simple, if it will be used in production for real, I will enhance it with 
- Exception handling
- Provide more content-negotiation formatters and test them
- Provide implementation of `DocumentRepository` with db context with connection to db
- Enhance caching timer/method by the simulation of big loads of requests to API
