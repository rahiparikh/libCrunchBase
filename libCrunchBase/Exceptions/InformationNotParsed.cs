/*
 * Copyright © Rahil Parikh 2012
 * 
 * Dual-Licensed - you may choose between
 * 
 * 1. Microsoft Reciprocal License
 * 2. CC BY 3.0
 * 
 * This file is part of libCrunchBase.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrunchBase.Exceptions
{
    class InformationNotParsed : Exception
    {
        public InformationNotParsed() : base() { }
        public InformationNotParsed(string Message) : base(Message) { }
    }
}
