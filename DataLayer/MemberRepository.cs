using BusinessLayer;
using System.Collections.Generic;

namespace DataAccess
{
    public class MemberRepository : IMemberRepository
    {
        MemberDAO memberDAO = new();
        void IMemberRepository.Add(Member member) => memberDAO.AddMember(member);

        void IMemberRepository.Delete(int ID) => memberDAO.DeleteMember(ID);

        Member IMemberRepository.GetMemberByID(int ID) => memberDAO.GetMember(ID);

        IEnumerable<Member> IMemberRepository.GetMembers() => memberDAO.GetMembers;

        Member IMemberRepository.Login(string email, string password) => memberDAO.Login(email, password);

        void IMemberRepository.Update(Member member) => memberDAO.UpdateMember(member);
    }
}
