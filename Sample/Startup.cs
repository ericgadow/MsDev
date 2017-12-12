﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Json;
using Nancy.Owin;
using Nancy.TinyIoc;
using Owin;

[assembly: OwinStartup(typeof(Sample.Startup))]

namespace Sample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
                .UseNancy(options => options.PassThroughWhenStatusCodesAre(
                    HttpStatusCode.NotFound,
                    HttpStatusCode.InternalServerError))
                .UseStageMarker(PipelineStage.MapHandler);
        }
    }

    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            JsonSettings.MaxJsonLength = Int32.MaxValue;
        }
    }
}
