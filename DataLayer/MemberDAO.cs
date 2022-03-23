using BusinessLayer;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class MemberDAO
    {
        private readonly List<Member> Members = new()
        {
            new Member(1, "Anna", "anna@gmail.com", "anna", "Las Vegas", "US"),
            new Member(2, "John", "John@gmail.com", "john", "New York", "US")
        };

        public List<Member> GetMembers => Members;

        public Member Login(string email, string password)
        {
            foreach (Member m in Members)
            {
                if (m.Email.Equals(email) && m.Password.Equals(password))
                {
                    return m;
                }
            }
            return null;
        }

        public Member GetMember(int ID)
        {
            foreach (Member m in Members)
            {
                if (m.MemberID.Equals(ID))
                {
                    return m;
                }
            }
            return null;
        }

        public void AddMember(Member newMember)
        {
            if (GetMember(newMember.MemberID) == null && Login(newMember.Email, newMember.Password) == null)
            {
                Members.Add(newMember);
            }
            else
            {
                throw new Exception("Member existed!");
            }
        }

        public void UpdateMember(Member member)
        {
            Member mem = GetMember(member.MemberID);
            if (mem != null)
            {
                int index = Members.IndexOf(mem);
                Members[index] = member;
            }
            else
            {
                throw new Exception("Member is not exist!");
            }
        }

        public void DeleteMember(int ID)
        {
            Member member = GetMember(ID);
            if (member != null)
            {
                Members.Remove(member);
            }
            else
            {
                throw new Exception("Member is not exist!");
            }
        }
    }
}
