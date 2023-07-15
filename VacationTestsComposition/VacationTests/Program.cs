using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml;
using NUnit.Engine;

namespace VacationTests
{
    /// <summary>
    ///     Класс для запуска тестов на ulearn
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            RunResult result;
            try
            {
                var newOut = new StringWriter();
                var oldOut = Console.Out;
                Console.SetOut(newOut);
                try
                {
                    using (var engine = TestEngineActivator.CreateInstance())
                    {
                        var package = new TestPackage(Assembly.GetExecutingAssembly().Location);
                        var filter = new TestFilter($"<filter><class>{args[0]}</class></filter>");
                        using (var runner = engine.GetRunner(package))
                        {
                            var testsResult = runner.Run(null, filter);
                            var passed = Convert.ToDouble(testsResult.Attributes["passed"].Value);
                            var countTests = Convert.ToDouble(testsResult.Attributes["total"].Value);
                            if (passed == countTests)
                            {
                                result = new(Verdict.Ok, $"All {countTests} tests are passed\n\n");
                            }
                            else
                            {
                                var failedReasons = new Dictionary<string, string>();

                                var nodes = testsResult.SelectNodes(".//test-case").Cast<XmlNode>()
                                    .Where(node => node.Attributes["result"].Value == "Failed");
                                foreach (var node in nodes)
                                {
                                    var message = node.SelectSingleNode("failure/message").InnerText;
                                    var stacktrace = node.SelectSingleNode("failure/stack-trace").InnerText;
                                    failedReasons.Add(node.Attributes["name"].Value, message + stacktrace);
                                }

                                var outputReason = string.Join("\n\n",
                                    failedReasons.Select(x => $"TestName = {x.Key}, Failed output = {x.Value}")
                                        .ToArray());
                                result = new(Verdict.RuntimeError,
                                    $"Passed {passed} tests from {countTests}:\n\n{outputReason}");
                            }
                        }
                    }
                }
                finally
                {
                    Console.SetOut(oldOut);
                }
            }
            catch (Exception e)
            {
                result = new(Verdict.RuntimeError, e.ToString());
            }

            Console.Write(JsonSerializer.Serialize(result));
        }
    }

    public enum Verdict
    {
        Ok = 1, // означает, что всё штатно протестировалось. Возвращается в том числе если тесты не прошли
        RuntimeError = 3
    }

    public class RunResult
    {
        public RunResult(Verdict verdict, string output = "")
        {
            Verdict = verdict;
            Output = output ?? "";
        }

        public Verdict Verdict { get; set; }
        public string Output { get; set; }
    }
}