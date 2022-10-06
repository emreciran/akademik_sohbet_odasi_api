using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TagManager : ITagService
    {
        ITagRepository _tagRepository;

        public TagManager(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task Delete(int id)
        {
            await _tagRepository.Delete(id);
        }

        public async Task<Tag> GetByID(int id)
        {
            return await _tagRepository.GetByID(id);
        }

        public async Task<List<Tag>> GetListAll()
        {
            return await _tagRepository.GetListAll();
        }

        public async Task<Tag> GetTagByName(string tagName)
        {
            return await _tagRepository.GetTagByName(tagName);
        }

        public async Task<ICollection<Tag>> GetTagByQuestion(int questionId)
        {
            return await _tagRepository.GetTagByQuestion(questionId);
        }

        public async Task<Tag> Insert(Tag t)
        {
            return await _tagRepository.Insert(t);
        }

        public async Task<Tag> Update(Tag t)
        {
            return await _tagRepository.Update(t);
        }
    }
}
