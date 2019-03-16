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

using SharpLife.CommandSystem;
using System;

namespace SharpLife.Engine.Server
{
    /// <summary>
    /// Handles all server specific engine operations, manages server state
    /// </summary>
    internal sealed class EngineServer
    {
        private readonly Host.Engine _engine;

        public ICommandContext CommandContext { get; }

        public EngineServer(Host.Engine engine)
        {
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));

            CommandContext = _engine.CommandSystem.CreateContext("ServerContext", _engine.EngineContext);
        }

        public void Shutdown()
        {
            CommandContext.Dispose();
        }
    }
}
