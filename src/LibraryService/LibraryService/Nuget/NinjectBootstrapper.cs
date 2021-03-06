﻿using System;
using Ninject;

namespace LibraryService
{
    public static class NinjectBootstrapper
    {
        private static readonly Lazy<IKernel> _kernel = new Lazy<IKernel>(() => new StandardKernel(new Bindings()));

        public static IKernel Kernel
        {
            get
            {
                return _kernel.Value;
            }
        } 
    }
}