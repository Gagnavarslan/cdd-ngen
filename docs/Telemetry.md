# Telemetry

## General and CD client-product related benefits
- General usage statistics|understanding; unused|misused features, unknown user scenarios; US impl priorities, etc.
- New product release health
- Resources distribution; APM to some extent; env res usage info, etc.
- Modules health check; crash notification; app damage(incorrect work) not visible to user, e.g. CDDS|Update service startup failure.
- Prodduct(versions) maintenance.
- Issue pattern-ing, foreseen degradation.

## Pseudo components
__RemoteAnalyzer__(_RA_) - _values_ delivery final point.  
__DeliveryMap__ - describes delivery route from _values_ origin using 0+ 'mid-points' to RA. Composition(linked list) of ordered DeliveryIntervals.  
__DeliveryTransition__ - delivery interval roughly described as 'from'-'transport'-'to'.  
__DeliveryTransport__ - [MQTT](https://en.wikipedia.org/wiki/MQTT) implementation, e.g. [written in C#](http://www.eclipse.org/paho/clients/dotnet/).  
~~[__commented out:__ Product's scope] __ValuesAlert(Auditor)__ - _values_ 'treshold-strategy' alert, generated locally before delivery to _RA_.~~  
__UserDataFilters__ - exclusion _data_ filters, based on user opt choices (for 'stable' product release).  
__DataCollector__ - _data_ sensor of product node being monitored. Produces _values_ using:
- ~~[__commented out:__ internal part of DataCollector] __Trigger__~~
- __Transformation__ - produces _values_ based on app _data_. It's also responsible for masking 'personal' _data_.
- ~~[__commented out:__ Product's scope] __ValuesAlertAuditor__ - local(within product) alert analyzer and producer. It might affect delivery map?~~
- __RemoteTelecomAgent__ - a) delivery map router|builder and delivery runner. b) listener and runner of _RA_ telecommands.

## Tech implementation
One of existing message service libraries([1](http://masstransit-project.com/MassTransit/), [2](https://github.com/nats-io/csharp-nats)) to be choosen.
Data collecting, filtering, transformation are not(or partially) covered and to be implemented within product.

## Misc
- _data_ - product usage data.
- _values_(metrics) - _data_ transformed with respect to 'minimization', 'integrity', 'confidentiality' principles.
- _values_ must be __portable__ - accessible for i) manual delivery ii) investigation of a related issue on-site.
- it make sense to differentiate usage data 'minimization' - 'in-house' product releases should not take into account user opt-out-s(GDPR?).
- [GDPR practical dev guide.](https://techblog.bozho.net/gdpr-practical-guide-developers/)

## Alt solutions
- [Application Insights](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-create-new-resource#create-an-application-insights-resource-1)
- [OpenTracing](https://github.com/opentracing-contrib/csharp-netcore)

## Useful links
- [__.NET MQTT client and a MQTT server (broker)__](https://github.com/chkr1011/MQTTnet)
- [DiagnosticSource User's Guide](https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/DiagnosticSourceUsersGuide.md)
- [Activity User Guide](https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/ActivityUserGuide.md)
- [Messaging with MQTT and .NET](https://developers.de/2018/07/23/mqtt-and-net/)
- [C# client library for subscribing/publishing MQTT](https://stackoverflow.com/questions/6635440/c-sharp-client-library-for-subscribing-publishing-mqtt-really-small-message-bro)
- [Implement MQTT client using C# to connect external MQTT broker](https://stackoverflow.com/questions/36653405/implement-mqtt-client-using-c-sharp-to-connect-external-mqtt-broker?rq=1)
