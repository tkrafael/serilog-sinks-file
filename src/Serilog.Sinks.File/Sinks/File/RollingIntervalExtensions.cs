// Copyright 2017 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace Serilog.Sinks.File
{
    static class RollingIntervalExtensions
    {
        public static string GetFormat(this RollingInterval interval)
        {
            switch (interval)
            {
                case RollingInterval.Infinite:
                    return "";
                case RollingInterval.Year:
                    return "yyyy";
                case RollingInterval.Month:
                    return "yyyyMM";
                case RollingInterval.Week:
                    return "yyyyMMdd";
                case RollingInterval.Day:
                    return "yyyyMMdd";
                case RollingInterval.Hour:
                    return "yyyyMMddHH";
                case RollingInterval.Minute:
                case RollingInterval.FiveMinutes:
                case RollingInterval.TenMinutes:
                case RollingInterval.QuarterHour:
                case RollingInterval.HalfHour:
                    return "yyyyMMddHHmm";
                default:
                    throw new ArgumentException("Invalid rolling interval");
            }
        }

        public static DateTime? GetCurrentCheckpoint(this RollingInterval interval, DateTime instant)
        {
            switch (interval)
            {
                case RollingInterval.Infinite:
                    return null;
                case RollingInterval.Year:
                    return new DateTime(instant.Year, 1, 1, 0, 0, 0, instant.Kind);
                case RollingInterval.Month:
                    return new DateTime(instant.Year, instant.Month, 1, 0, 0, 0, instant.Kind);
                case RollingInterval.Week:
                    return new DateTime(instant.Year, instant.Month, instant.Day, 0, 0, 0, instant.Kind);
                case RollingInterval.Day:
                    return new DateTime(instant.Year, instant.Month, instant.Day, 0, 0, 0, instant.Kind);
                case RollingInterval.Hour:
                    return new DateTime(instant.Year, instant.Month, instant.Day, instant.Hour, 0, 0, instant.Kind);
                case RollingInterval.Minute:
                    return new DateTime(instant.Year, instant.Month, instant.Day, instant.Hour, instant.Minute, 0, instant.Kind);
                case RollingInterval.FiveMinutes:
                    return new DateTime(instant.Year, instant.Month, instant.Day, instant.Hour, (int)(instant.Minute / 5) * 5, 0, instant.Kind);
                case RollingInterval.TenMinutes:
                    return new DateTime(instant.Year, instant.Month, instant.Day, instant.Hour, (int)(instant.Minute / 10) * 10, 0, instant.Kind);
                case RollingInterval.QuarterHour:
                    return new DateTime(instant.Year, instant.Month, instant.Day, instant.Hour, (int)(instant.Minute / 15) * 15, 0, instant.Kind);
                case RollingInterval.HalfHour:
                    return new DateTime(instant.Year, instant.Month, instant.Day, instant.Hour, (int)(instant.Minute / 30) * 30, 0, instant.Kind);
                default:
                    throw new ArgumentException("Invalid rolling interval");
            }
        }

        public static DateTime? GetNextCheckpoint(this RollingInterval interval, DateTime instant)
        {
            var current = GetCurrentCheckpoint(interval, instant);
            if (current == null)
                return null;

            switch (interval)
            {
                case RollingInterval.Year:
                    return current.Value.AddYears(1);
                case RollingInterval.Month:
                    return current.Value.AddMonths(1);
                case RollingInterval.Week:
                    return current.Value.AddDays(7);
                case RollingInterval.Day:
                    return current.Value.AddDays(1);
                case RollingInterval.Hour:
                    return current.Value.AddHours(1);
                case RollingInterval.Minute:
                    return current.Value.AddMinutes(1);
                case RollingInterval.FiveMinutes:
                    return current.Value.AddMinutes(5);
                case RollingInterval.TenMinutes:
                    return current.Value.AddMinutes(10);
                case RollingInterval.QuarterHour:
                    return current.Value.AddMinutes(15);
                case RollingInterval.HalfHour:
                    return current.Value.AddMinutes(30);
                default:
                    throw new ArgumentException("Invalid rolling interval");
            }
        }
    }
}
