using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{   
    public enum EAttrForge
    {
        Attack,
        Defence,
        Health,
        HitSpeed,
        Speed,
        Range,
        Money,
    }

    public class AttrForge
    {
        private float _baseValue;
        private float _curValue;

        private List<IAttrModifier> _modifiers;

        public AttrForge(float baseValue)
        {
            _baseValue = baseValue;
            _curValue = _baseValue;
            _modifiers = new List<IAttrModifier>();
        }

        /// <summary>
        /// 属性初始化
        /// </summary>
        /// <param name="value">值</param>
        public void Init(float value)
        {
            _baseValue = value;
            _curValue = _baseValue;
        }

        /// <summary>
        /// 获取基础值
        /// </summary>
        /// <typeparam name="T">int/float</typeparam>
        /// <returns>值</returns>
        public T BaseValue<T>() where T : IConvertible => (T)Convert.ChangeType(_baseValue, typeof(T));

        /// <summary>
        /// 获取当前值
        /// </summary>
        /// <typeparam name="T">int/float</typeparam>
        /// <returns>值</returns>
        public T CurValue<T>() where T : IConvertible => (T) Convert.ChangeType(_curValue, typeof(T));

        /// <summary>
        /// 更新基础值
        /// </summary>
        /// <typeparam name="T">int/float</typeparam>
        /// <param name="value">更新值</param>
        /// <returns>更新后的值</returns>
        public T UpdateBaseValue<T>(float value) where T : IConvertible
        {
            _baseValue += value;

            return (T)Convert.ChangeType(_baseValue, typeof(T));
        }

        /// <summary>
        /// 更新当前值
        /// </summary>
        /// <typeparam name="T">int/float</typeparam>
        /// <param name="value">更新值</param>
        /// <returns>更新后的值</returns>
        public T UpdateCurValue<T>(float value) where T : IConvertible
        {
            _curValue += value;

            return (T)Convert.ChangeType(_curValue, typeof(T));
        }

        /// <summary>
        /// 添加属性修改器
        /// </summary>
        /// <param name="modifier">属性修改器</param>
        /// <returns>是否添加成功</returns>
        public bool AddModifier(IAttrModifier modifier)
        {
            _modifiers.Add(modifier);
            CalculateCurValue();

            return true;
        }

        /// <summary>
        /// 移除属性修改器
        /// </summary>
        /// <param name="modifier">属性修改器</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveModifier(IAttrModifier modifier)
        {
            _modifiers.Remove(modifier);
            CalculateCurValue();

            return true;
        }

        /// <summary>
        /// 清空属性修改器
        /// </summary>
        public void ClearModifier()
        {
            _modifiers.Clear();
            _curValue = _baseValue;
        }

        private void CalculateCurValue()
        {
            float result = _baseValue;

            float flatSum = 0f;
            float percentSum = 0f;

            foreach (var m in _modifiers)
            {
                if (m.IsPercent)
                {
                    percentSum += m.Value;
                }
                else
                {
                    flatSum += m.Value;
                }
            }

            result += flatSum;
            result *= (1 + percentSum);

            _curValue = result;
        }
    }
}