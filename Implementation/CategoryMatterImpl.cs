using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class CategoryMatterImpl : CategoryMatterDao
    {
        public void Delete(CategoryMatter t)
        {
            string queryCategory = @"DELETE CategoryMatter WHERE CategoryMatterId = @CategoryMatterId";
            string queryMatter = @"DELETE Matter WHERE CategoryMatterId = @CategoryMatterId";
            try
            {
                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(2);
                cmds[0].CommandText = queryMatter;
                cmds[0].Parameters.AddWithValue("@CategoryMatterId", t.CategoryId);
                cmds[1].CommandText = queryCategory;
                cmds[1].Parameters.AddWithValue("@CategoryMatterId", t.CategoryId);

                DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} | Error:  Could not Dele Class({1}).", DateTime.Now, ex.Message));
            }
        }

        public int Insert(CategoryMatter t)
        {
            string query = @"INSERT INTO CategoryMatter (CategoryName,status,ModalityId) VALUES
	 (@name,1,(SELECT s.ModalityId  FROM School s WHERE s.SchoolId=@id))";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);

                cmd.Parameters.AddWithValue("@id", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@name", t.CategoryName);

                return DBImplementation.ExecuteBasicCommand(cmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Select()
        {
            string query = @"SELECT CategoryMatterId AS 'ID', CategoryName AS 'Name', ModalityId AS 'Modalidad'
FROM CategoryMatter cm 
WHERE cm.ModalityId = (SELECT m.ModalityId FROM Modality m INNER JOIN School s ON s.ModalityId=m.ModalityId WHERE s.SchoolId = @SchoolId)
order by CategoryName";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectLike(string like)
        {
            string query = @"SELECT CategoryMatterId AS 'ID', CategoryName AS 'Name', ModalityId AS 'Modalidad'
FROM CategoryMatter cm 
WHERE cm.ModalityId = (SELECT m.ModalityId FROM Modality m INNER JOIN School s ON s.ModalityId=m.ModalityId WHERE s.SchoolId = @SchoolId)
AND cm.CategoryName LIKE @like
order by CategoryName";
            try
            {
                SqlCommand cmd = DBImplementation.CreateBasicComand(query);
                cmd.Parameters.AddWithValue("@SchoolId", Session.SessionSchoolId);
                cmd.Parameters.AddWithValue("@like", "%" + like + "%");
                return DBImplementation.ExecuteDataTableCommand(cmd);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Update(CategoryMatter t)
        {
            throw new NotImplementedException();
        }

        public void updateCategory(List<CategoryMatter> categoryMatters)
        {
            string queryCategory = @"UPDATE CategoryMatter SET CategoryName = @CategoryName WHERE CategoryMatterId = @id";
            try
            {

                List<SqlCommand> cmds = DBImplementation.CreateNBasicCommands(categoryMatters.Count);

                for (int i=0; i<categoryMatters.Count;i++)
                {
                    cmds[i].CommandText = queryCategory;
                    cmds[i].Parameters.AddWithValue("@id", categoryMatters[i].CategoryId);
                    cmds[i].Parameters.AddWithValue("@CategoryName", categoryMatters[i].CategoryName);
                }
                    DBImplementation.ExecuteNBasicCommand(cmds);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
