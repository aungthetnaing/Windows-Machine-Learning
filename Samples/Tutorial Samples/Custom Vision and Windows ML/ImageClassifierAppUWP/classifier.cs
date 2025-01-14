// This file was automatically generated by VS extension Windows Machine Learning Code Generator v3
// from model file classifier.onnx
// Warning: This file may get overwritten if you add add an onnx file with the same name
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.AI.MachineLearning;
namespace ImageClassifierAppUWP
{
    
    public sealed class classifierInput
    {
        public ImageFeatureValue data; // BitmapPixelFormat: Bgra8, BitmapAlphaMode: Premultiplied, width: 224, height: 224
    }
    
    public sealed class classifierOutput
    {
        public TensorString classLabel; // shape(-1,1)
        public IList<IDictionary<string,float>> loss;
    }
    
    public sealed class classifierModel
    {
        private LearningModel model;
        private LearningModelSession session;
        private LearningModelBinding binding;
        public static async Task<classifierModel> CreateFromStreamAsync(IRandomAccessStreamReference stream)
        {
            classifierModel learningModel = new classifierModel();
            learningModel.model = await LearningModel.LoadFromStreamAsync(stream);

            // Select GPU or another DirectX device to evaluate the model.
            LearningModelDevice device = new LearningModelDevice(LearningModelDeviceKind.DirectX);
            
            // Create the evaluation session with the model and device.
            learningModel.session = new LearningModelSession(learningModel.model, device);
            learningModel.binding = new LearningModelBinding(learningModel.session);
            return learningModel;
        }
        public async Task<classifierOutput> EvaluateAsync(classifierInput input)
        {
            binding.Bind("data", input.data);
            var result = await session.EvaluateAsync(binding, "0");
            var output = new classifierOutput();
            output.classLabel = result.Outputs["classLabel"] as TensorString;
            output.loss = result.Outputs["loss"] as IList<IDictionary<string,float>>;
            return output;
        }
    }
}

