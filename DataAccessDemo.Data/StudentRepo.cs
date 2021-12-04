using System;
using System.Collections.Generic;
using System.Linq;
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

        //This is a destructor (DTOR) it fires once when the object is destroyed
        ~StudentRepo()
        {
            _documentStore.Dispose();
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

            Student student = session.Load<Student>(studentId);
            
          //  List<Student> allStudents = session.Query<Student>().ToList();
          //  List<Student> males = session.Query<Student>().Where(x=>x.Sex==Enums.Sex.Male).ToList();
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
