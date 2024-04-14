using Model.Config;
using Utilities.Ocr.Implement;

namespace Utilities.Ocr;

public class OcrFactory {
    public static IOcrProduct Create(OcrConfig ocrConfig) {
        IOcrProduct ocr;
        return ocrConfig.Type switch {
            OcrType.BAIDU_STD => new BaiduOcr(ocrConfig),
            _ => new BaiduOcr(ocrConfig)
        };
    }

}
