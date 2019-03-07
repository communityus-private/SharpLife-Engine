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

using SharpLife.CommandSystem.Commands.VariableFilters;
using System;
using System.Collections.Generic;

namespace SharpLife.CommandSystem.Commands
{
    /// <summary>
    /// Contains information about a command variable
    /// A command variable has a value of a specific type, specified as type parameter <typeparamref name="T"/>
    /// </summary>
    public sealed class VariableInfo<T> : BaseCommandInfo<VariableInfo<T>>
    {
        internal readonly List<VariableChangeHandler<T>> _onChangeDelegates = new List<VariableChangeHandler<T>>();

        public IReadOnlyList<VariableChangeHandler<T>> ChangeHandlers => _onChangeDelegates;

        private VariableFiltersBuilder<T> _filters;

        public VariableFiltersBuilder<T> Filters => _filters ?? (_filters = new VariableFiltersBuilder<T>(this));

        internal bool HasFilters => _filters?.HasFilters == true;

        public T Value { get; }

        public VariableInfo(string name, in T defaultValue = default)
            : base(name)
        {
            Value = defaultValue;
        }

        public VariableInfo<T> WithChangeHandler(VariableChangeHandler<T> changeHandler)
        {
            if (changeHandler == null)
            {
                throw new ArgumentNullException(nameof(changeHandler));
            }

            _onChangeDelegates.Add(changeHandler);

            return this;
        }
    }
}
