using Model.ViewModel.Config;
using Utilities.Ocr.Implement;

namespace Utilities.Ocr;

public class OcrFactory {
    public static IOcrProduct Create(OcrConfig ocrConfig) {
        IOcrProduct ocr;
        return ocrConfig.OcrType.Value switch {
            OcrType.BAIDU_STD => new BaiduOcr(ocrConfig),
            _ => new BaiduOcr(ocrConfig)
        };
    }

}
