using System;
using DataAccessDemo.Data.Dto;
using Marten;
using Marten.NodaTime;
using Marten.PLv8;

namespace DataAccessDemo.Data
{
    public class StudentRepo
    {
        private readonly DocumentStore _documentStore;

        public StudentRepo()
        {
            _documentStore = GetDocumentStore();
        }

        public void Save(Student studentToSave)
        {
            using IDocumentSession session = _documentStore.OpenSession();
            
            session.Store(studentToSave);
            session.SaveChanges();
        }

        public Student Get(Guid studentId)
        {
            using IDocumentSession session = _documentStore.OpenSession();

            var student = session.Load<Student>(studentId);
            return student;
        }
        
        public void Delete(Guid studentId)
        {
            using IDocumentSession session = _documentStore.OpenSession();

            session.Delete<Student>(studentId);
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
