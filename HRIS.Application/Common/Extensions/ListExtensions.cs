using HRIS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRIS.Application.Common.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Finds the entity to soft delete and updates found soft deletable entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void SoftRemove<T>(this List<T> list, T item)
        {
            var filtered = list.Where(q => ((dynamic)q).Id == ((dynamic)item).Id).FirstOrDefault();

            // omits validation, etc.
            if (!filtered.GetType().IsSubclassOf(typeof(SoftDeletableEntity)))
            {
                throw new ArgumentException("Soft deletion works only for SoftDeletableEntity types", "entity");
            }



            var _softDeletableEntity = filtered as SoftDeletableEntity;
            _softDeletableEntity.IsDeleted = true;
            _softDeletableEntity.DeletedDate = DateTime.Now;
            _softDeletableEntity.DeletedBy = "Backend";
        }
        /// <summary>
        /// Softdeletes List of Soft deletable entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void SoftRemove<T>(this List<T> list)
        {
            // omits validation, etc.

            foreach(var e in list)
            {
                if (!e.GetType().IsSubclassOf(typeof(SoftDeletableEntity)))
                {
                    throw new ArgumentException("Soft deletion works only for SoftDeletableEntity types", "entity");
                }
            }

            foreach (var e in list)
            {
                var _softDeletableEntity = e as SoftDeletableEntity;
                _softDeletableEntity.IsDeleted = true;
                _softDeletableEntity.DeletedDate = DateTime.Now;
                _softDeletableEntity.DeletedBy = "Backend";
            }
            
        }
        /// <summary>
        /// Softdeletes single Soft deletable entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void SoftRemove<T>(this T list)
        {
            // omits validation, etc.
            if (!list.GetType().IsSubclassOf(typeof(SoftDeletableEntity)))
            {
                throw new ArgumentException("Soft deletion works only for SoftDeletableEntity types", "entity");
            }

            var _softDeletableEntity = list as SoftDeletableEntity;
            _softDeletableEntity.IsDeleted = true;
            _softDeletableEntity.DeletedDate = DateTime.Now;
            _softDeletableEntity.DeletedBy = "Backend";
        }
    }
}
