using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mongo2Go;
using MongoDB.Driver;

namespace ControleDeGastos.Tests
{
    public class MongoDBInfraBaseTests
    {
        const string MONGO_DB_TESTES = "TesteDB";
        protected static MongoDbRunner _runner;
        protected static IMongoDatabase mongoDB;
        protected static IMongoCollection<Gasto> collectionGastos;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            _runner = MongoDbRunner.Start();
            var mongoClient = new MongoClient(_runner.ConnectionString);
            mongoDB = mongoClient.GetDatabase(MONGO_DB_TESTES);
            collectionGastos = mongoDB.GetCollection<Gasto>(nameof(Gasto));
        }

        [TestCleanup]
        public virtual void TestCleanup() => _runner.Dispose();
    }
}