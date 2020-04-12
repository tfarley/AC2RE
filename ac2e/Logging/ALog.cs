using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public static class ALog {

    public interface Category {

        string name { get; }
        bool disabledByDefault { get; }
    }

    public enum Level {
        TRACE,
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL
    }

    public static Level level = Level.TRACE;
    public static bool timestamps = true;

    private static readonly HashSet<Category> knownCategories = new HashSet<Category>();
    private static readonly HashSet<Category> disabledCategories = new HashSet<Category>();

    private static string nowString => DateTime.Now.ToString("s", System.Globalization.CultureInfo.InvariantCulture);

    public static void setCategoryEnabled(Category category, bool enabled) {
        knownCategories.Add(category);
        if (enabled) {
            disabledCategories.Remove(category);
        } else {
            disabledCategories.Add(category);
        }
    }

    private static bool checkCategory(Category category) {
        if (category != null && knownCategories.Add(category)) {
            if (category.disabledByDefault) {
                disabledCategories.Add(category);
                return false;
            }
        }
        return !disabledCategories.Contains(category);
    }

    private static string buildLogLine(Category category, string type, string str) {
        StringBuilder sb = new StringBuilder();
        if (timestamps) {
            sb.Append(nowString);
            sb.Append(' ');
        }
        if (category != null) {
            sb.Append('[');
            sb.Append(category.name);
            sb.Append("] ");
        }
        sb.Append(type);
        sb.Append(": ");
        sb.Append(str);
        return sb.ToString();
    }

    [Conditional("DEBUG")]
    public static void trace(object obj, Category category = null) {
        trace(obj.ToString(), category);
    }

    [Conditional("DEBUG")]
    public static void trace(string str, Category category = null) {
        if (level > Level.TRACE || !checkCategory(category)) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "TRACE", str));
    }

    [Conditional("DEBUG")]
    public static void trace(string format, Category category = null, params object[] args) {
        if (level > Level.TRACE || !checkCategory(category)) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "TRACE", format), args);
    }

    [Conditional("DEBUG")]
    public static void debug(object obj, Category category = null) {
        debug(obj.ToString(), category);
    }

    [Conditional("DEBUG")]
    public static void debug(string str, Category category = null) {
        if (level > Level.DEBUG || !checkCategory(category)) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "DEBUG", str));
    }

    [Conditional("DEBUG")]
    public static void debug(string format, Category category = null, params object[] args) {
        if (level > Level.DEBUG || !checkCategory(category)) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "DEBUG", format), args);
    }

    public static void info(object obj, Category category = null) {
        info(obj.ToString(), category);
    }

    public static void info(string str, Category category = null) {
        if (level > Level.INFO || !checkCategory(category)) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "INFO", str));
    }

    public static void info(string format, Category category = null, params object[] args) {
        if (level > Level.INFO || !checkCategory(category)) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "INFO", format), args);
    }

    public static void warn(object obj, Category category = null) {
        warn(obj.ToString(), category);
    }

    public static void warn(string str, Category category = null) {
        if (level > Level.WARN || !checkCategory(category)) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "WARNING", str));
    }

    public static void warn(string format, Category category = null, params object[] args) {
        if (level > Level.WARN || !checkCategory(category)) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "WARNING", format), args);
    }

    public static void error(object obj, Category category = null) {
        error(obj.ToString(), category);
    }

    public static void error(string str, Category category = null) {
        if (level > Level.ERROR) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "ERROR", str));
    }

    public static void error(string format, Category category = null, params object[] args) {
        if (level > Level.ERROR) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "ERROR", format), args);
    }

    public static void exception(Exception e, Category category = null) {
        if (level > Level.ERROR) {
            return;
        }

        Console.WriteLine(e);
    }

    public static void fatal(object obj, Category category = null) {
        fatal(obj.ToString(), category);
    }

    public static void fatal(string str, Category category = null) {
        if (level > Level.FATAL) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "FATAL", str));
    }

    public static void fatal(string format, Category category = null, params object[] args) {
        if (level > Level.FATAL) {
            return;
        }

        Console.WriteLine(buildLogLine(category, "FATAL", format), args);
    }
}
