using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Monads;
using Accord.Imaging;
using Accord.MachineLearning;
using Accord.Statistics.Kernels;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using System.Collections.Generic;
using System;

namespace ImageOrganizer.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public ObservableCollection<ImageViewModel> SourceFiles { get; }
        public ObservableCollection<TargetFolderViewModel> TargetDirectories { get; }

        public double Tolerance => 0.01;
        public int CacheSize => 500;
        public IKernel Kernel => new ChiSquare();

        public MainWindowViewModel()
        {
            SourceFiles = new ObservableCollection<ImageViewModel>();
            TargetDirectories = new ObservableCollection<TargetFolderViewModel>();

            SourceFiles.CollectionChanged += SourceFiles_CollectionChanged;
        }

        public static readonly BinarySplit binarySplit = new BinarySplit(100);
        public static BagOfVisualWords BOW = new BagOfVisualWords(binarySplit);
        
        private MulticlassSupportVectorMachine _ksvm;

        private void SourceFiles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (_ksvm == null)
                {
                    InitializeBagOfWords();

                    TargetDirectories.Do(d => d.CalculateSampleFeatures(BOW));

                    var inputs = new List<double[]>();
                    var outputs = new List<int>();

                    GatherData(inputs, outputs);
                    CreateAndTrainKSVM(inputs, outputs);
                }

                var imageVM = ((ImageViewModel)e.NewItems[0]);
                imageVM.CalculateFeatureVector(BOW);
                double score = 0.0f;
                var actual = _ksvm.Compute(imageVM.FeatureVector, out score);
                imageVM.CurrentTarget = TargetDirectories.First(d => d.FolderId == actual);
                imageVM.HighestScore = score;
            }
        }

        private void InitializeBagOfWords()
        {
            BOW.Compute(TargetDirectories.SelectMany(d => d.Images.Select(i => i.BitmapImage)).ToArray());
        }

        private void GatherData(IList<double[]> inputs, IList<int> outputs)
        {
            Console.WriteLine("Gathering data.");
            foreach (var dir in TargetDirectories)
            {
                foreach (var sample in dir.SampleFeatures)
                {
                    inputs.Add(sample);
                    outputs.Add(dir.FolderId);
                }
            }
            Console.WriteLine("Data gathered");
        }

        private void CreateAndTrainKSVM(IList<double[]> inputs, IList<int> outputs)
        {
            _ksvm = new MulticlassSupportVectorMachine(inputs[0].Length, Kernel, TargetDirectories.Count);
            MulticlassSupportVectorLearning ml = new MulticlassSupportVectorLearning(_ksvm, inputs.ToArray(), outputs.ToArray());
            
            double complexity = SequentialMinimalOptimization.EstimateComplexity(Kernel, inputs.ToArray());
            SelectionStrategy strategy = SelectionStrategy.Sequential;
            ml.Algorithm = (svm, classInputs, classOutputs, i, j) =>
            {
                return new SequentialMinimalOptimization(svm, classInputs, classOutputs)
                {
                    Complexity = complexity,
                    Tolerance = Tolerance,
                    CacheSize = CacheSize,
                    Strategy = strategy,
                };
            };

            Console.WriteLine("Starting SVM training");
            ml.Run();
            Console.WriteLine("SVM trained");
        }
    }
}
