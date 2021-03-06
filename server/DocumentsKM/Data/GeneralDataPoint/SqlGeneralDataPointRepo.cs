using System.Collections.Generic;
using System.Linq;
using DocumentsKM.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsKM.Data
{
    public class SqlGeneralDataPointRepo : IGeneralDataPointRepo
    {
        private readonly ApplicationContext _context;

        public SqlGeneralDataPointRepo(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<GeneralDataPoint> GetAllByUserAndSectionId(
            int userId, int sectionId)
        {
            return _context.GeneralDataPoints.Where(
                v => v.User.Id == userId && v.Section.Id == sectionId).OrderBy(
                    v => v.OrderNum).ToList();
        }

        public GeneralDataPoint GetById(int id)
        {
            return _context.GeneralDataPoints.SingleOrDefault(v => v.Id == id);
        }

        public GeneralDataPoint GetByUniqueKey(
            int userId, int sectionId, string text)
        {
            return _context.GeneralDataPoints.SingleOrDefault(
                v => v.User.Id == userId && v.Section.Id == sectionId && v.Text == text);
        }

        public void Add(GeneralDataPoint generalDataPoint)
        {
            _context.GeneralDataPoints.Add(generalDataPoint);
            _context.SaveChanges();
        }

        public void Update(GeneralDataPoint generalDataPoint)
        {
            _context.Entry(generalDataPoint).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(GeneralDataPoint generalDataPoint)
        {
            _context.GeneralDataPoints.Remove(generalDataPoint);
            _context.SaveChanges();
        }
    }
}
