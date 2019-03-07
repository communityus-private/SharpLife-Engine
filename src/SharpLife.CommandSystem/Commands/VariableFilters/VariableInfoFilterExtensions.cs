﻿/***
*
*	Copyright (c) 1996-2001, Valve LLC. All rights reserved.
*	
*	This product contains software technology licensed from Id 
*	Software, Inc. ("Id Technology").  Id Technology (c) 1996 Id Software, Inc. 
*	All Rights Reserved.
*
*   This source code contains proprietary and confidential information of
*   Valve LLC and its suppliers.  Access to this code is restricted to
*   persons who have executed a written SDK license with Valve.  Any access,
*   use or distribution of this code by or to any unlicensed person is illegal.
*
****/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SharpLife.CommandSystem.Commands.VariableFilters
{
    /// <summary>
    /// Extensions to make adding filters to variables easier
    /// </summary>
    public static class VariableInfoFilterExtensions
    {
        /// <summary>
        /// <see cref="NumberSignFilter(bool)"/>
        /// </summary>
        /// <param name="this"></param>
        /// <param name="positive"></param>
        /// <returns></returns>
        public static VariableInfo<T> WithNumberSignFilter<T>(this VariableInfo<T> @this, bool positive)
            where T : IComparable<T>
        {
            return @this.WithFilter(new NumberSignFilter<T>(positive));
        }

        public static VariableInfo<T> WithMinMaxFilter<T>(this VariableInfo<T> @this, T? min, T? max, bool denyOutOfRangeValues = false)
            where T : struct, IComparable<T>, IEquatable<T>
        {
            return @this.WithFilter(new MinMaxFilter<T>(min, max, denyOutOfRangeValues));
        }

        public static VariableInfo<string> WithRegexFilter(this VariableInfo<string> @this, Regex regex)
        {
            return @this.WithFilter(new RegexFilter(regex));
        }

        public static VariableInfo<string> WithRegexFilter(this VariableInfo<string> @this, string pattern)
        {
            return @this.WithFilter(new RegexFilter(new Regex(pattern)));
        }

        public static VariableInfo<string> WithStringListFilter(this VariableInfo<string> @this, IReadOnlyList<string> strings)
        {
            return @this.WithFilter(new StringListFilter(strings));
        }

        public static VariableInfo<string> WithStringListFilter(this VariableInfo<string> @this, params string[] strings)
        {
            return @this.WithFilter(new StringListFilter(strings));
        }

        public static VariableInfo<T> WithInvertedFilter<T>(this VariableInfo<T> @this, IVariableFilter<T> filter)
        {
            return @this.WithFilter(new InvertFilter<T>(filter));
        }

        public static VariableInfo<T> WithDelegateFilter<T>(this VariableInfo<T> @this, DelegateFilter<T>.FilterDelegate @delegate)
        {
            return @this.WithFilter(new DelegateFilter<T>(@delegate));
        }

        public static VariableInfo<string> WithPrintableCharactersFilter(this VariableInfo<string> @this, string emptyValue = "")
        {
            return @this.WithFilter(new UnprintableCharactersFilter(emptyValue));
        }

        public static VariableInfo<string> WithWhitespaceFilter(this VariableInfo<string> @this)
        {
            return @this.WithFilter(new StripWhitespaceFilter());
        }
    }
}
