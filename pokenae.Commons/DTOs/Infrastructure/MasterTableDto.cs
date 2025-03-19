using System;
using System.ComponentModel.DataAnnotations;

namespace pokenae.Commons.DTOs
{
    public abstract class MasterTableDto : InfrastructureDto
    {
        private static readonly Dictionary<int, MasterTableDto> _cache = new();
        /// <summary>
        /// �p����ŃL���b�V���̃L�[�ƂȂ�l��yield return�ŕԂ�
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetCacheKeys();

        public static MasterTableDto GetFromCache(int id)
        {
            _cache.TryGetValue(id, out var dto);
            return dto;
        }

        public static void AddToCache(int id, MasterTableDto dto)
        {
            _cache[id] = dto;
        }

        public static void RemoveFromCache(int id)
        {
            _cache.Remove(id);
        }
    }
}
