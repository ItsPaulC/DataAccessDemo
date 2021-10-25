using System;
using DataAccessDemo.Data.Dto;
using Marten;
using Marten.NodaTime;
using Marten.PLv8;

namespace DataAccessDemo.Data
{
    public class CompanyRepo
    {
        private readonly DocumentStore _documentStore;

        public CompanyRepo()
        {
            _documentStore = GetDocumentStore();
        }

        public void Save(Company companyToSave)
        {
            using IDocumentSession session = _documentStore.OpenSession();
            
            session.Store(companyToSave);
            session.SaveChanges();
        }

        public Company Get(Guid companyId)
        {
            using IDocumentSession session = _documentStore.OpenSession();

            var company = session.Load<Company>(companyId);
            return company;
        }
        
        public void Delete(Guid companyId)
        {
            using IDocumentSession session = _documentStore.OpenSession();

            session.Delete<Company>(companyId);
            session.SaveChanges();
        }

        private DocumentStore GetDocumentStore()
        {
            var store = DocumentStore
                .For(_ =>
                {
                    _.Connection("host=localhost;database=postgres;password=admin;username=admin");
                    _.UseNodaTime();
                    _.UseJavascriptTransformsAndPatching();
                });

            return store;
        }
    }
}
