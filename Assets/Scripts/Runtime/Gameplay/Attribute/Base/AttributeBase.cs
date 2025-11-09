using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{   
    public enum EAttrType
    {
        Ice = 0,
    }

    public abstract class AttributeBase<T> where T : DataBase
    {
        protected int[] _attrTypesArry;
        protected Dictionary<EAttrForge, AttrForge> _attrForgeDic;

        public AttributeBase()
        {
            _attrTypesArry = new int[64];
            _attrForgeDic = new Dictionary<EAttrForge, AttrForge>();
        }

        public abstract void Init(T data);

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="eAttrForge">属性类型</param>
        /// <param name="forgeInstance">属性实例</param>
        public void AddAttrForge(EAttrForge eAttrForge, AttrForge forgeInstance)
        {
            _attrForgeDic.Add(eAttrForge, forgeInstance);
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="eAttrForge">属性类型</param>
        /// <returns>属性实例</returns>
        public AttrForge GetAttrForge(EAttrForge eAttrForge) => _attrForgeDic[eAttrForge];

        /// <summary>
        /// 状态初始化
        /// </summary>
        /// <param name="types">需要初始化的状态</param>
        public void AttrTypesInit(List<EAttrType> types)
        {
            foreach (var type in types)
            {
                IncreaseAttrType(type, true);
            }
        }

        /// <summary>
        /// 是否处于状态
        /// </summary>
        /// <param name="type">状态类型</param>
        /// <returns>是否处于</returns>
        public bool IsAttrType(EAttrType type)
        {
            return _attrTypesArry[(int)type] > 0;
        }

        /// <summary>
        /// 状态计数器增加
        /// </summary>
        /// <param name="type">状态类型</param>
        /// <param name="value">值</param>
        /// <returns>计数器值</returns>
        public int IncreaseAttrType(EAttrType type, bool stack, int value = 1)
        {   
            if (!stack)
            {
                return _attrTypesArry[(int)type] = 1;
            }

            return _attrTypesArry[(int)type] += value;
        }

        /// <summary>
        /// 状态计数器减少
        /// </summary>
        /// <param name="type">状态类型</param>
        /// <param name="value">值</param>
        /// <returns>计数器值</returns>
        public int DecreaseAttrType(EAttrType type, int value = 1)
        {
            return _attrTypesArry[(int)type] = Mathf.Max(0, _attrTypesArry[(int)type] - value);
        }

        /// <summary>
        /// 状态计数器重置
        /// </summary>
        /// <param name="type">状态类型</param>
        public void ClearAttrType(EAttrType type)
        {
            _attrTypesArry[(int)type] = 0;
        }

        public void ClearAllModifier()
        {
            foreach (var attr in _attrForgeDic.Values)
            {
                attr.ClearModifier();
            }
        }
    }
}