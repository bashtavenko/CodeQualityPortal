using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeQualityPortal.Data
{
    public class SampleDataSeeder
    {
        public SampleDataSeeder(CodeQualityContext context)
        {
            _context = context;
            _commitsCircularBuffer = new CircularBuffer<DimCommit>(_commits);
        }

        private readonly CodeQualityContext _context;
        private readonly List<DimModule> _modules = new List<DimModule>
        {
            new DimModule
            {
                Name = "LegacyMutexTester.dll",
                AssemblyVersion = "1.0.0.0",
                FileVersion = "1.0.0.0",
                Namespaces = new List<DimNamespace>
                {
                    new DimNamespace
                    {
                        Name = "LegacyMutexTester",
                        Types = new List<DimType>
                        {
                            new DimType
                            {
                                Name = "RecursiveLolcatSet",
                                Members = new List<DimMember>
                                {
                                    new DimMember { Name = "Label.set(string) : void", File = "PayService.cs", Line = 23 },
                                    new DimMember { Name = "GetPaymentStatus(GetPaymentStatusRequest) : GetPaymentStatusResponse", File = "Account.cs", Line = 7 },
                                }
                            }
                        },
                    }
                }
            }
        };

        private readonly List<DimRepo> _repo = new List<DimRepo>
        {
            new DimRepo { Name = "Github-master" },
            new DimRepo { Name = "Bitbucket-master" }
        };


        private readonly List<DimCommit> _commits = new List<DimCommit>
        {
            new DimCommit
            {
                Sha = "21bbb99934a36899c3c65347ea886569823c9a54",
                Url = "https://github.com/StanBPublic/CodeMetricsLoader/commit/21bbb99934a36899c3c65347ea886569823c9a54",
                Committer = "StanBPublic", CommitterAvatarUrl = "https://avatars.githubusercontent.com/u/1820912?v=3", Message = "Added FileName field to DimTarget",
                Files = new List<DimFile>
                {
                    new DimFile { FileName = "CodeMetricsLoader.Tests/IntegrationTests/ContextTests.cs", FileExtension = ".cs" },
                    new DimFile { FileName = "CodeMetricsLoader.Tests/IntegrationTests/LoaderTests.cs", FileExtension = ".cs" }
                }
            },
            new DimCommit
            {
                Sha = "9c4800fdeb47aa8f990105fd894ab1f125efc51e",
                Url = "https://github.com/StanBPublic/CodeMetricsLoader/commit/9c4800fdeb47aa8f990105fd894ab1f125efc51e",
                Committer = "StanBPublic", CommitterAvatarUrl = "https://avatars.githubusercontent.com/u/1820912?v=3", Message = "Formatting changes",
                Files = new List<DimFile>
                {
                    new DimFile { FileName = "CodeMetricsLoader/Data/Context.cs", FileExtension = ".cs" },
                    new DimFile { FileName = "CodeMetricsLoader/app.config", FileExtension = ".config" }
                }
            },
            new DimCommit
            {
                Sha = "9c4800fdeb47aa8f990105fd894ab1f125efc51e",
                Url = "https://github.com/StanBPublic/CodeMetricsLoader/commit/9c4800fdeb47aa8f990105fd894ab1f125efc51e",
                Committer = "StanBPublic", CommitterAvatarUrl = "https://avatars.githubusercontent.com/u/1820912?v=3", Message = "Null reference bug fixed",
                Files = new List<DimFile>
                {
                    new DimFile { FileName = "CodeMetricsLoader/Data/Mapper.cs", FileExtension = ".cs" },
                }
            },
            new DimCommit
            {
                Sha = "0e9a2e544e360364556bcf2288566af271784fce",
                Url = "https://bitbucket.org/api/2.0/repositories/stanbpublic/codequalityportal/diff/0e9a2e544e360364556bcf2288566af271784fce",
                Committer = "StanBPublic", CommitterAvatarUrl = "https://bitbucket-assetroot.s3.amazonaws.com/c/photos/2014/Oct/12/stanbpublic-avatar-1580570491-7_avatar.png", Message = "Added TopX stats\n",
                Files = new List<DimFile>
                {
                    new DimFile { FileName = "CodeQualityPortal/MetricsRepository.cs", FileExtension = ".cs" },
                }
            },
        };

        private readonly CircularBuffer<DimCommit> _commitsCircularBuffer;
        
        public void Seed()
        {
            var today = DateTime.Now;
            for (int i = 7; i >= 0; i--)
            {
                var date = new DimDate(today.AddDays(i*-1));
                SeedMetrics(date);
                SeedChurn(date);
            }

            _context.SaveChanges();
        }

        private void SeedMetrics(DimDate date)
        {
            foreach (var module in _modules)
            {
                FactMetrics metrics;

                foreach (var ns in module.Namespaces)
                {
                    foreach (var type in ns.Types)
                    {
                        foreach (var member in type.Members)
                        {
                            metrics = MakeMetrics(date.Date);
                            metrics.Member = member;
                            metrics.Type = type;
                            metrics.Namespace = ns;
                            metrics.Module = module;
                            metrics.Date = date;
                            member.Metrics.Add(metrics);
                        }
                        metrics = Aggregate(type.Members.SelectMany(s => s.Metrics).ToList(), date);
                        metrics.Type = type;
                        metrics.Namespace = ns;
                        metrics.Module = module;
                        metrics.Date = date;
                        type.Metrics.Add(metrics);
                    }

                    metrics = Aggregate(ns.Types.SelectMany(s => s.Metrics).ToList(), date);
                    metrics.Namespace = ns;
                    metrics.Module = module;
                    metrics.Date = date;
                    ns.Metrics.Add(metrics);
                }

                metrics = Aggregate(module.Namespaces.SelectMany(s => s.Metrics).ToList(), date);
                metrics.Module = module;
                metrics.Date = date;
                module.Metrics.Add(metrics);
                _context.Modules.Add(module);
            }
        }

        private void SeedChurn(DimDate date)
        {
            FactCodeChurn churn;
            foreach (var repo in _repo)
            {
                var commit = _commitsCircularBuffer.GetNext();
                foreach (var file in commit.Files)
                {
                    churn = MakeChurn(date.Date);
                    churn.File = file;
                    churn.Commit = commit;
                    churn.Date = date;
                    file.Churn.Add(churn);
                }
                churn = Aggregate(commit.Files.SelectMany(s => s.Churn), date);
                churn.Commit = commit;
                churn.Date = date;
                commit.Date = date.Date;
                commit.Churn.Add(churn);
                repo.Commits.Add(commit);
                _context.Repos.Add(repo);
            }
        }    

        /// <summary>
        /// Aggregates given metrics
        /// </summary>
        /// <param name="input">list of metrics </param>
        /// <param name="date">date of run</param>
        /// <returns>Aggregation</returns>
        private FactMetrics Aggregate(IEnumerable<FactMetrics> input, DimDate date)
        {
            var dailyInput = input.Where(w => w.Date.Date == date.Date).ToList();
            
            return new FactMetrics
            {
                MaintainabilityIndex = Convert.ToInt32(dailyInput.Average(s => s.MaintainabilityIndex)),
                LinesOfCode = Convert.ToInt32(dailyInput.Sum(s => s.LinesOfCode)),
                CyclomaticComplexity = Convert.ToInt32(dailyInput.Sum(s => s.CyclomaticComplexity)),
                ClassCoupling = Convert.ToInt32(dailyInput.Sum(s => s.ClassCoupling)),
                DepthOfInheritance = Convert.ToInt32(dailyInput.Average(s => s.DepthOfInheritance)),
                CodeCoverage = Convert.ToInt32(dailyInput.Average(s => s.CodeCoverage)),
            };
        }

        private FactCodeChurn Aggregate(IEnumerable<FactCodeChurn> input, DimDate date)
        {
            var dailyInput = input.Where(w => w.Date.Date == date.Date).ToList();

            return new FactCodeChurn
            {
                LinesAdded = Convert.ToInt32(dailyInput.Sum(s => s.LinesAdded)),
                LinesDeleted = Convert.ToInt32(dailyInput.Sum(s => s.LinesDeleted)),
                TotalChurn = Convert.ToInt32(dailyInput.Sum(s => s.TotalChurn))
            };
        }

        private FactMetrics MakeMetrics(DateTime date)
        {
            int value = date.Day; 
            
            return new FactMetrics
            {
                MaintainabilityIndex = value * 2,
                CyclomaticComplexity = value * 30,
                ClassCoupling = value % 4 + 1,
                DepthOfInheritance = value % 5 + 1,
                LinesOfCode = value * 30,
                CodeCoverage = value
            };
        }

        private FactCodeChurn MakeChurn(DateTime date)
        {
            int value = date.Day;

            return new FactCodeChurn
            {
                LinesAdded = value * 5,
                LinesDeleted = value,
                TotalChurn = value * 6
            };
        }

        class CircularBuffer<T>
        {
            private readonly List<T> _items;
            private int _lastIndex;
            public CircularBuffer(List<T> items)
            {

                _items = items;
                _lastIndex = 0;
            }

            public int Length { get { return _items.Count; } }

            public T GetNext()
            {

                _lastIndex = _lastIndex < _items.Count ? _lastIndex : 0;
                return _items[_lastIndex++];
            }
        }
    }
}