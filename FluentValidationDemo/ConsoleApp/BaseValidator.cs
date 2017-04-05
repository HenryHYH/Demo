using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ConsoleApp
{
    /// <summary>
    /// 校验规则基类
    /// </summary>
    /// <typeparam name="T">需要校验的类型</typeparam>
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        private const string RequiredLengthRuleMessage = "{PropertyName}为非空且长度不超过{0}的字符串";
        private const string NonRequiredLengthRuleMessage = "{PropertyName}长度不超过{0}的字符串";
        private const string RequiredFixedLengthRuleMessage = "{PropertyName}为非空且长度为{0}的字符串";
        private const string NumberRuleMessage = "{PropertyName}必须不能小于等于0";
        private const string ArrayRuleMessage = "{PropertyName}必须为{0}中的一个";

        public BaseValidator()
        {
            // 修复{PropertyName}中可能会包含空格的问题
            ValidatorOptions.DisplayNameResolver = (type, member, exp) => { return member.Name; };
        }

        /// <summary>
        /// 必填字段长度必须在[min, max]范围内
        /// </summary>
        /// <param name="expression">字段</param>
        /// <param name="min">最小值，默认为0</param>
        /// <param name="max">最大值，默认为30</param>
        /// <returns>规则链</returns>
        public IRuleBuilderOptions<T, string> RequiredLengthRuleFor(Expression<Func<T, string>> expression, int min = 0, int max = 30)
        {
            return RuleFor(expression).NotEmpty()
                                      .WithMessage(RequiredLengthRuleMessage, max)
                                      .Must(x => min <= GetLength(x) && GetLength(x) <= max)
                                      .WithMessage(RequiredLengthRuleMessage, max);
        }

        /// <summary>
        /// 非必填字段长度必须在[min, max]范围内
        /// </summary>
        /// <param name="expression">字段</param>
        /// <param name="min">最小值，默认为0</param>
        /// <param name="max">最大值，默认为30</param>
        /// <returns>规则链</returns>
        public IRuleBuilderOptions<T, string> NonRequiredLengthRuleFor(Expression<Func<T, string>> expression, int min = 0, int max = 30)
        {
            return RuleFor(expression).Must(x => string.IsNullOrEmpty(x) || (min <= GetLength(x) && GetLength(x) <= max))
                                      .WithMessage(NonRequiredLengthRuleMessage, max);
        }

        /// <summary>
        /// 必填字段长度必须为length
        /// </summary>
        /// <param name="expression">字段</param>
        /// <param name="length">固定长度</param>
        /// <returns>规则链</returns>
        public IRuleBuilderOptions<T, string> RequiredFixedLengthRuleFor(Expression<Func<T, string>> expression, int length)
        {
            return RuleFor(expression).Must(x => length == GetLength(x))
                                      .WithMessage(RequiredFixedLengthRuleMessage, length);
        }

        /// <summary>
        /// 字段必须大于等于0
        /// </summary>
        /// <param name="expression">字段</param>
        /// <returns>规则链</returns>
        public IRuleBuilderOptions<T, decimal> NumberRuleFor(Expression<Func<T, decimal>> expression)
        {
            return RuleFor(expression).LessThan(0)
                                      .WithMessage(NumberRuleMessage);
        }

        /// <summary>
        /// 字段必须在指定元素中
        /// </summary>
        /// <typeparam name="TProperty">字段类型</typeparam>
        /// <param name="expression">字段</param>
        /// <param name="array">指定元素</param>
        /// <returns>规则链</returns>
        public IRuleBuilderOptions<T, TProperty> ArrayRuleFor<TProperty>(Expression<Func<T, TProperty>> expression, TProperty[] array)
            where TProperty : IComparable
        {
            return RuleFor(expression).Must(x => null == array || array.Any(y => 0 == y.CompareTo(x)))
                                      .WithMessage(ArrayRuleMessage, string.Join(",", array));
        }

        /// <summary>
        /// 获取字符串长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>长度</returns>
        private int GetLength(string str)
        {
            return Encoding.Default.GetByteCount(str);
        }
    }
}
