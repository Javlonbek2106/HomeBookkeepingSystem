﻿namespace HomeBookkeeping.Application.Common.Models;
public record ExcelReportResponse(byte[] FileContents, string Option, string FileName);