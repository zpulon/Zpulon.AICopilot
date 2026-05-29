using System;

namespace Zpulon.AICopilot.Services.Common.Exceptions;

public class ForbiddenException(string? message) : Exception(message);