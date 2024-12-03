using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public enum QRCodeErrorCorrectionLevel { L, M, Q, H } // 列挙型の名前を変更
public enum ErrorCorrectionLevel { L, M, Q, H } // 
public class QRCodeGenerator : UdonSharpBehaviour
{
    // QRコードのバージョン
    public int qrCodeVersion = 1;

    // エラー訂正レベル
    public QRCodeErrorCorrectionLevel errorCorrectionLevel = QRCodeErrorCorrectionLevel.M;

    // 生成する文字列
    public string VarHoldersText = "";

    // テクスチャ
    public Texture2D qrCodeTexture;

    // マテリアル
    public Material targetMaterial;

    // オブジェクト
    public GameObject outo_logObject;
    public GameObject VarHolderObject;

    private int[] antilogTable = new int[256]; // 0から255までの256個の要素
    private int[] logTable = new int[256];     // 0から255までの256個の要素
        
    private void Start()
    {
    }

 
    void Update()
    {
        // ここで文字列を取得する。
        // instanceの抽出
        VarHolder VarHolderScript = VarHolderObject.GetComponent<VarHolder>();
        Frame_text frame_Text = outo_logObject.GetComponent<Frame_text>();

        // 文字列が更新された場合、qrコードを生成
        if (VarHoldersText != VarHolderScript.ReadStrData())
        {
            VarHoldersText = VarHolderScript.ReadStrData();
            // QRCodeGenerate(VarHoldersText);        
            QRCodeGenerate(VarHoldersText); // test
                    
        }

    }

    // 確認中
    private void QRCodeGenerate(string data)
    {
        // ここで、ガロア体（GF(2^8)）の計算に必要な対数(log)テーブルと
        // 逆対数(antilog)テーブルを生成しています。
        // ガロア体のテーブルを用意することで、QRコード生成時のエラー訂正コード計算などで
        // 高速に乗算や除算を行えるようになります。
        // このテーブルは、データのエンコードやエラー訂正コードの生成など、
        // QRコードを生成するためのさまざまな計算で使用されます。
        GenerateGaloisFieldTables();

        // オブジェクトからインスタンスを抽出
        Frame_text frame_Text = outo_logObject.GetComponent<Frame_text>();

        // 文字列の確認
        Debug.Log(frame_Text.WriteStr("【文字列を用意】"));
        Debug.Log(frame_Text.WriteStr(data));

        // 確認済み
        // エンコードモードの決定
        string mode = DetermineEncodeMode(data);
        frame_Text.WriteStr($"\n【エンコードモードを自動選択】\n{mode}\n");

        // 確認済み
        // データのエンコード
        byte[] encodedData;
        switch (mode)
        {
            case "Numeric":
                frame_Text.WriteStr($"【入力されたデータは数字でした。エンコードタイプをNumericでエンコードをしました】");
                encodedData = EncodeNumericData(data);
                frame_Text.WriteByteArray(encodedData);
                break;

            case "Alphanumeric":
                frame_Text.WriteStr($"【入力されたデータはアルファベットでした。エンコードタイプをAlphanumericでエンコードをしました】");
                encodedData = EncodeAlphanumericData(data);
                frame_Text.WriteByteArray(encodedData);
                break;

            default:
                frame_Text.WriteStr($"【入力されたコーパスは様々なタイプのキャラクターが混ざっていました。そのためASCIIコードとしてエンコードをしました】");
                encodedData = EncodeByteData(data);
                frame_Text.WriteByteArray(encodedData);
                break;
        }
        // 上記で大量のリストがprintされています。せめて説明を加えてください。



        // 確認中
        // エラー訂正コードを生成する関数を呼び出す(エラー補正レベルをL,M,Q,Hから選択可能)
        frame_Text.WriteStr("\n【エラー訂正codeを生成】");
        byte[] errorCorrectionCode = GenerateReedSolomonErrorCorrectionCode(encodedData, ErrorCorrectionLevel.M); // ここがおかしい
        // frame_Text.WriteStr("\nエラー訂正コードに使った入力↓");
        // frame_Text.WriteByteArray(encodedData);
        // frame_Text.WriteStr("\nエラー訂正コード↓");
        // frame_Text.WriteByteArray(errorCorrectionCode);



        

        // frame_Text.WriteStr("\nエラー訂正コードを生成する関数を呼び出す(エラー補正レベルをL,M,Q,Hから選択可能)");
        // frame_Text.WriteByteArray(errorCorrectionCode);

        // Debug.Log("\nエラー訂正コードを生成する関数を呼び出す(エラー補正レベルをL,M,Q,Hから選択可能)");
        // Debug.Log(frame_Text.WriteByteArray(errorCorrectionCode));

        frame_Text.WriteStr("\nテスト");


        // // データとエラー訂正コードを組み合わせる
        // byte[] combinedData = CombineDataAndErrorCorrection(encodedData, errorCorrectionCode);
        // frame_Text.WriteStr("\nデータとエラー訂正コードを組み合わせる");
        // frame_Text.WriteByteArray(combinedData);
        // Debug.Log("\nデータとエラー訂正コードを組み合わせる");
        // Debug.Log(frame_Text.WriteByteArray(combinedData));

        // // ここでQRコードのマトリックスを生成
        // bool[][] qrCodeMatrix = CreateQRCodeMatrix(combinedData);

        // // ステップ1: 8種類のマスクパターンを用意し、適用する
        // bool[][][] maskedMatrices = ApplyMaskPatterns(qrCodeMatrix);

        // // ステップ2: 各マスクされたマトリックスに対してペナルティスコアを計算
        // int[] penaltyScores = CalculatePenaltyScores(maskedMatrices);

        // // ステップ3: 最適なマスクパターンの選択
        // int bestMaskIndex = SelectBestMask(penaltyScores);

        // // ここでフォーマット情報のエンコードを行う
        // byte[] formatInformation = EncodeFormatInformation(errorCorrectionLevel, bestMaskIndex);

        // フォーマット情報をマトリックスに配置する処理をここに実装 (未実装)



        // // // ステップ4: 最適なマスクを適用したマトリックスの生成
        // // bool[][] finalMatrix = ApplyBestMask(qrCodeMatrix, bestMaskIndex);

        // // 画質を向上させる
        // bool[][] expandedMatrix = CreateExpandedQRCodeMatrix(qrCodeMatrix);
        // frame_Text.WriteStr("\n2次元のQRコード配列の画質を向上させる");
        // frame_Text.WriteBool2DArray(qrCodeMatrix);

        // // クワイエットゾーン（余白）の追加。
        // bool[][] qrCodeWithQuietZone = CreateQRCodeWithQuietZone(expandedMatrix);
        // frame_Text.WriteStr("\nクワイエットゾーン（余白）の追加。");
        // frame_Text.WriteBool2DArray(qrCodeWithQuietZone);

        // // QRコード配列をTexture2Dに変換する(ディスプレイ用)
        // ConvertMatrixToTexture2D(qrCodeWithQuietZone);
    }


    byte[] EncodeData(int[] aVec)
    {
    
        // エンコーディング後のデータの最大サイズを予測して配列を初期化
        // 100は追加のバッファサイズで、必要に応じて調整する
        byte[] encodedData = new byte[aVec.Length + 100]; 
        int index = 0;
    
        // データの長さをバイナリ形式で追加(最初のバイトとして)
        encodedData[index++] = (byte)aVec.Length;
    
        // 各文字(ASCIIコード)をバイナリ形式で追加
        foreach (int element in aVec)
        {
            encodedData[index++] = (byte)element; // ASCIIコードをバイトとして追加
        }
    
        // パディングを追加する関数を呼び出し
        AddPadding(encodedData, ref index);
    
        // 実際のデータ長に合わせて配列をトリミング
        byte[] finalData = new byte[index];
        for (int i = 0; i < index; i++)
        {
            finalData[i] = encodedData[i];
        }
    
        return finalData;
    }
    void AddPadding(byte[] encodedData, ref int index)
    {
        // QRコードの規格に基づいてパディングを追加
        byte[] paddingBytes = { 0xEC, 0x11 }; // '11101100' と '00010001' のバイト表現
        int paddingIndex = 0;
    
        // データが適切な長さになるまでパディングを追加
        while (index % 8 != 0 || !IsCorrectLength(index))
        {
            encodedData[index++] = paddingBytes[paddingIndex]; // パディングバイトを追加
            paddingIndex = (paddingIndex + 1) % paddingBytes.Length; // 次のパディングバイトを選択
        }
    }
    bool IsCorrectLength(int length)
    {
        // ここでは、エンコードされたデータがQRコードの特定の規格に合う長さかどうかをチェック
        // 実際のQRコードのバージョンやエラー訂正レベルに応じた適切な長さを判断
        // この例では仮の条件を使用
        return length >= 8; // 仮の条件
    }

    // 確認中2024年5月10日
    // ガロア体GF(2^8)での多項式の除算メソッド
    int[] DividePolynomials(int[] dividend, int[] divisor) {
    
        Frame_text frame_Text = outo_logObject.GetComponent<Frame_text>();

    
        // 商を保存するための配列を準備。配列の大きさは被除数の長さと同じです。(被除数→割られる数)
        // 例: dividend = [0, 3, 1, 4] -> quotient = [0, 0, 0, 0] (初期状態)
        int[] quotient = new int[dividend.Length];
        Debug.Log(frame_Text.WriteStr("\n【商を保存するための空の変数(入力arrayと同じlen数の空のarray型)を用意】"));
        Debug.Log(frame_Text.WriteIntArray(quotient));

        // 除算の過程で更新される「余り」を保存する配列。最初は被除数のコピーからスタート。
        // 例: dividend = [0, 3, 1, 4] -> remainder = [0, 3, 1, 4] (初期状態)
        // この配列は 3x^2 + 1x + 4 を表す。
        int[] remainder = new int[dividend.Length];
        for (int i = 0; i < dividend.Length; i++) {
            remainder[i] = dividend[i];
        }
        Debug.Log(frame_Text.WriteStr("\n【除算の過程で更新される「モジュロ演算の答え」を保存する配列。最初は被除数のコピーとする。】"));
        Debug.Log(frame_Text.WriteIntArray(remainder));


        // 除数の多項式で最初に0でない係数が現れる位置を見つけます。
        // この係数は除算の計算開始点として重要です。たとえば、[0, 0, 1, 2]の場合、
        // 最初の非ゼロ係数は1で、これが多項式 x^2 + 2のx^2の係数に相当します。
        // 例: divisor = [0, 0, 1, 2] -> divisorLeadingCoefficientIndex = 2 (最初の非ゼロ係数のindex)
        // この配列は多項式 x^2 + 2 を表しており、最高次係数は1でその位置は配列の2番目です。
        // 最初の非ゼロ係数のインデックスを見つけるために、配列の最後から探します
        int divisorLeadingCoefficientIndex = divisor.Length - 1;
        while (divisorLeadingCoefficientIndex >= 0 && divisor[divisorLeadingCoefficientIndex] == 0) {
            divisorLeadingCoefficientIndex--;
        }
        if (divisorLeadingCoefficientIndex == -1) { // 全ての係数が0の場合のエラーチェック
            Debug.LogError("除数が無効です。全ての係数が0です。");
            return null; // エラー処理
        }


        Debug.Log(frame_Text.WriteStr("\ndivisor配列の、最初の非ゼロ係数のindex（1行目divisor,2行目index）"));
        Debug.Log(frame_Text.WriteIntArray(divisor));
        Debug.Log(frame_Text.WriteInt(divisorLeadingCoefficientIndex));


        int quotientLength = 0; // 商の項数をカウントします。(初期状態では0)
        int divisorDegree = divisor.Length - divisorLeadingCoefficientIndex - 1;
        for (int i = 0; i <= dividend.Length - divisor.Length; i++) {
            int currentQuotient = GaloisFieldDivide(remainder[i], divisor[divisorLeadingCoefficientIndex]);
            quotient[quotientLength++] = currentQuotient;

            if (currentQuotient != 0) {
                for (int j = 0; j < divisor.Length - divisorLeadingCoefficientIndex; j++) {
                    remainder[i + j] ^= GaloisFieldMultiply(divisor[j + divisorLeadingCoefficientIndex], currentQuotient);
                }
            }
        }
        // quotientLength（商の項数）
        // 商の項数を数えるための変数だよ。
        // 割り算をするとき、この変数は割り算の結果の数、つまり「商」の項の数を数えるために増えていくんだ。
        // 例えば：
        // 割られる数(dividend)が [0, 3, 1, 4] で
        // 割る数(divisor)が [1, 2] の場合、
        // 結果の商は最大で3つの数字（項）からなることがあります。
        // この割り算をするたびに quotientLength は1ずつ増えて、
        // 最終的には商の数を表すことになるよ。

        // divisorLeadingCoefficientIndex（除数の最初の非ゼロ係数の位置）
        // 除数の中で最初に0ではない数が出てくる場所を示す変数だよ。
        // 割り算を始める前に、どこから割り算を始めるかを決めるために重要なんだ。
        // 例えば：割る数が [0, 0, 1, 2] の場合、最初の0ではない数は「1」で、これはリストの3番目（インデックス2）に位置しているね。

        Debug.Log(frame_Text.WriteStr("\n商の項数をカウントしました。\n割られる数\n割る数\n商の項の数\n最初の非ゼロのindex"));
        Debug.Log(frame_Text.WriteIntArray(dividend));
        Debug.Log(frame_Text.WriteIntArray(divisor));
        Debug.Log(frame_Text.WriteInt(quotientLength));
        Debug.Log(frame_Text.WriteInt(divisorLeadingCoefficientIndex));

        // quotientを実際の長さにトリミング
        int[] trimmedQuotient = new int[quotientLength];
        System.Array.Copy(quotient, 0, trimmedQuotient, 0, quotientLength);

        return trimmedQuotient;
    }


    // ガロア体GF(2^8)での除算を行うメソッド
    int GaloisFieldDivide(int a, int b)
    {
        // 0で割ることはできないため、その場合はエラー処理が必要
        if (b == 0) {
            // 例外を投げる代わりに、特定のエラー値を返すか、エラーログを出力する
            Debug.LogError("Divide by zero in GaloisFieldDivide");
            return 0; // 0を返すか、適切なエラー処理をここに実装
        }
        if (a == 0) {
            return 0;
        }

        int logA = logTable[a];
        int logB = logTable[b];
        int logResult = (logA - logB + 255) % 255;
        return antilogTable[logResult];
    }



    // 多項式の最高次の次数を求めるヘルパーメソッド
    int GetPolynomialDegree(int[] polynomial)
    {
        for (int i = polynomial.Length - 1; i >= 0; i--)
        {
            if (polynomial[i] != 0)
                return i;
        }
        return 0;
    }


    // 確認中
    // Reed-Solomonエラー訂正コードの生成
    public byte[] GenerateReedSolomonErrorCorrectionCode(byte[] data, ErrorCorrectionLevel level)
    {
        // オブジェクトからインスタンスを抽出
        Frame_text frame_Text = outo_logObject.GetComponent<Frame_text>();

        Debug.Log(frame_Text.WriteStr("\n==== エラー訂正コードの生成の開始 ===="));
        
        // 入力されたデータ
        Debug.Log(frame_Text.WriteStr("入力されたデータ"));
        Debug.Log(frame_Text.WriteByteArray(data));

        // ざっと確認済み
        // 1. データをポリノミアル形式に変換
        // 入力されたデータを、ポリノミアル（数学的な式）の形に変換したもの。
        int[] polynomialData = PreparePolynomialData(data); // 入力: byte[] data（生データ）, 出力: int[] polynomialData（データのポリノミアル表現）
        Debug.Log(frame_Text.WriteStr("\nデータをポリノミアル形式に変換"));
        Debug.Log(frame_Text.WriteIntArray(polynomialData));

        // ざっと確認済み
        // 2.エラー訂正コードの強さ（長さ）を決定します。この長さはエラー訂正レベル（L, M, Q, H）によって異なる
        int errorCorrectionCodeLength = DetermineErrorCorrectionCodeLength(level, data.Length); // 入力: ErrorCorrectionLevel level, int data.Length（データの長さ）, 出力: int errorCorrectionCodeLength（エラー訂正コードの長さ）
        Debug.Log(frame_Text.WriteStr("\nエラー訂正コードの強さ（長さ）は次の通りです。確認してください。"));
        Debug.Log(frame_Text.WriteInt(errorCorrectionCodeLength));
        
        // 確認中「CalculateGeneratorPolynomialは、異なる入力でも、出力はいつも同じで大丈夫だそうです。」
        // 3. エラー訂正コードの長さに基づいて生成多項式を計算。エラー訂正コードの長さをもとに、生成多項式を計算します。
        int[] generatorPolynomial = CalculateGeneratorPolynomial(errorCorrectionCodeLength); // 入力: int errorCorrectionCodeLength, 出力: int[] generatorPolynomial（生成多項式の係数） // ここがおかしい
        Debug.Log(frame_Text.WriteStr("\nエラー訂正コードの長さに基づいて生成多項式を計算"));
        Debug.Log(frame_Text.WriteIntArray(generatorPolynomial));

        // 確認中
        // 4. エラー訂正ポリノミアルの生成: データポリノミアルを生成多項式で除算して得られる剰余がエラー訂正ポリノミアル
        int[] errorCorrectionPolynomial = DividePolynomials(polynomialData, generatorPolynomial); // ここがおかしい
        Debug.Log(frame_Text.WriteStr("\nエラー訂正ポリノミアルの生成（データポリノミアルを生成多項式で除算して得られる剰余がエラー訂正ポリノミアル）"));
        Debug.Log(frame_Text.WriteIntArray(errorCorrectionPolynomial));

        // 5.エラー訂正ポリノミアルの各項をバイト配列に変換。これが実際にQRコードに追加されるエラー訂正コードです。
        byte[] errorCorrectionBytes = new byte[errorCorrectionCodeLength];
        for (int i = 0; i < errorCorrectionPolynomial.Length; i++)
        {
            errorCorrectionBytes[i] = (byte)errorCorrectionPolynomial[i];
        }
        Debug.Log(frame_Text.WriteStr("\nエラー訂正ポリノミアルの各項をバイト配列に変換。これが実際にQRコードに追加されるエラー訂正コードです。"));
        Debug.Log(frame_Text.WriteByteArray(errorCorrectionBytes));

        return errorCorrectionBytes;
    }


    byte[] CombineDataAndErrorCorrection(byte[] encodedData, byte[] errorCorrectionCode)
    {
        // エンコードされたデータとエラー訂正コードの合計長さ
        int totalLength = encodedData.Length + errorCorrectionCode.Length;
        byte[] combinedData = new byte[totalLength];
    
        // エンコードされたデータを追加
        for (int i = 0; i < encodedData.Length; i++)
        {
            combinedData[i] = encodedData[i];
        }
    
        // エラー訂正コードを追加
        for (int i = 0; i < errorCorrectionCode.Length; i++)
        {
            combinedData[encodedData.Length + i] = errorCorrectionCode[i];
        }
    
        return combinedData;
    }
    bool[][] CreateQRCodeMatrix(byte[] combinedData)
    {
        int matrixSize = 21; // バージョン1なので21。
        bool[][] qrCodeMatrix = new bool[matrixSize][];

        for (int i = 0; i < matrixSize; i++)
        {
            qrCodeMatrix[i] = new bool[matrixSize];
            for (int j = 0; j < matrixSize; j++)
            {
                qrCodeMatrix[i][j] = false; // ここではすべてのブロックを白（false）とする
            }
        }

        //
        // combinedDataを元に、実際のQRコードのパターンをここで生成する
        //
        
        // ファインダーパターンの配置
        AddFinderPattern(qrCodeMatrix, 0, 0);
        AddFinderPattern(qrCodeMatrix, matrixSize - 7, 0);
        AddFinderPattern(qrCodeMatrix, 0, matrixSize - 7);

        // タイミングパターンの配置
        AddTimingPattern(qrCodeMatrix);

        // データとエラー訂正コードの配置
        PlaceDataAndErrorCorrectionCodes(qrCodeMatrix, combinedData);

        // 未実装
        // ファインダーパターン、タイミングパターンの配置後
        // フォーマット情報の配置を行う
        // 例: qrCodeMatrix = PlaceFormatInformation(qrCodeMatrix, encodedFormatInformation);

        return qrCodeMatrix;
    }

    int[] StringToAsciiArray(string str)
    {
        int[] asciiArray = new int[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            asciiArray[i] = (int)str[i];
        }
        return asciiArray;
    }
    void ConvertMatrixToTexture2D(bool[][] qrCodeMatrix)
    {
        // 既存のテクスチャのサイズを確認（このサイズは、UnityのEditorで設定しておく）
        int width = qrCodeTexture.width;
        int height = qrCodeTexture.height;

        // QRコードマトリックスをテクスチャに変換
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // マトリックスのサイズとテクスチャのサイズが異なる場合は、適応するロジックが必要
                int matrixI = i * qrCodeMatrix.Length / width;
                int matrixJ = j * qrCodeMatrix[matrixI].Length / height;

                qrCodeTexture.SetPixel(i, j, qrCodeMatrix[matrixI][matrixJ] ? Color.black : Color.white);
            }
        }

        qrCodeTexture.Apply(); // 変更を適用

        // このテクスチャを使用しているマテリアルを更新
        if (targetMaterial != null)
        {
            targetMaterial.mainTexture = qrCodeTexture;
        }
    }
    bool[][] CreateQRCodeWithQuietZone(bool[][] originalMatrix) {
        int quietZoneSize = 8; // クワイエットゾーンのサイズ
        int newSize = originalMatrix.Length + quietZoneSize * 2; // 新しいマトリックスのサイズ
        bool[][] newMatrix = new bool[newSize][];

        // 新しいマトリックスの初期化
        for (int i = 0; i < newSize; i++) {
            newMatrix[i] = new bool[newSize];
            for (int j = 0; j < newSize; j++) {
                // クワイエットゾーンの部分を白で埋める
                if (i < quietZoneSize || i >= newSize - quietZoneSize || j < quietZoneSize || j >= newSize - quietZoneSize) {
                    newMatrix[i][j] = false;
                } else {
                    // 元のQRコードデータを中心に配置
                    newMatrix[i][j] = originalMatrix[i - quietZoneSize][j - quietZoneSize];
                }
            }
        }

        return newMatrix;
    }
    bool[][] CreateExpandedQRCodeMatrix(bool[][] originalMatrix) // 画質をscaleFactor倍あげる関数
    {
        int originalSize = 21; // 元のマトリックスサイズ
        int scaleFactor = 4; // 拡大係数
        int newSize = originalSize * scaleFactor; // 新しいマトリックスサイズ
        bool[][] expandedMatrix = new bool[newSize][];

        for (int i = 0; i < newSize; i++)
        {
            expandedMatrix[i] = new bool[newSize];
            for (int j = 0; j < newSize; j++)
            {
                // 元のマトリックスの値に基づいて、新しい位置の値を設定
                expandedMatrix[i][j] = originalMatrix[i / scaleFactor][j / scaleFactor];
            }
        }

        return expandedMatrix;
    }
    void AddFinderPattern(bool[][] matrix, int startX, int startY)
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if ((i == 0 || i == 6) || (j == 0 || j == 6))
                    matrix[startX + i][startY + j] = true; // 外枠を黒に
                else if ((i >= 2 && i <= 4) && (j >= 2 && j <= 4))
                    matrix[startX + i][startY + j] = true; // 内枠を黒に
                else
                    matrix[startX + i][startY + j] = false; // 内部を白に
            }
        }
    }
    void AddTimingPattern(bool[][] matrix)
    {
        for (int i = 8; i < matrix.Length - 8; i++)
        {
            matrix[i][6] = i % 2 == 0;
            matrix[6][i] = i % 2 == 0;
        }
    }
    void PlaceDataAndErrorCorrectionCodes(bool[][] matrix, byte[] combinedData)
    {
        int size = matrix.Length; // マトリックスのサイズ
        int bitIndex = 0; // combinedDataのビットインデックス
        int direction = -1; // 初期方向は下向き (-1は下、1は上)
        int row = size - 1; // スタート位置はマトリックスの右下
        int col = size - 1; // 最後の列から始めるが、最初は左に2つ移動

        // マトリックスの右下から左上に向かってデータを配置
        while (col > 0) {
            if (col == 6) col -= 1; // タイミングパターンの列をスキップ

            for (int i = 0; i < size; i++) {
                int currentRow = direction == -1 ? (row - i) : (row + i - size + 1);

                // マトリックスの範囲内で、セルが予約されていない場合にビットを配置
                if (currentRow >= 0 && currentRow < size) {
                    if (!IsReserved(matrix, currentRow, col) && bitIndex < combinedData.Length * 8) {
                        matrix[currentRow][col] = ((combinedData[bitIndex / 8] >> (7 - (bitIndex % 8))) & 1) == 1;
                        bitIndex++;
                    }
                    if (col - 1 >= 0 && !IsReserved(matrix, currentRow, col - 1) && bitIndex < combinedData.Length * 8) {
                        matrix[currentRow][col - 1] = ((combinedData[bitIndex / 8] >> (7 - (bitIndex % 8))) & 1) == 1;
                        bitIndex++;
                    }
                }
            }

            col -= 2; // 次の2列に移動
            direction = -direction; // 方向を反転
        }

        // 未実装
        // データとエラー訂正コードの配置後に、フォーマット情報を配置するためのプレースホルダー
        // 実際のフォーマット情報の配置はデータ配置後に行われるべき
    }
    bool IsReserved(bool[][] matrix, int row, int col)
    {
        // これまでに実装したアライメントなどを避けて、メイン情報を載せるためのメソッド。

        // バージョン1のQRコードでのファインダーパターンとタイミングパターンの位置をチェック
        int size = matrix.Length;

        // ファインダーパターンの位置 (左上、右上、左下)
        if ((row < 8 && (col < 8 || col >= size - 8)) || (row >= size - 8 && col < 8))
        {
            return true; // ファインダーパターンの場所
        }

        // タイミングパターンの位置
        if (row == 6 || col == 6)
        {
            return true; // タイミングパターンの場所
        }

        // 他の機能的要素がある場合は、ここに追加

        return false; // それ以外の場所は予約されていない
    }

    // エンコードモードを決定するメソッド
    public string DetermineEncodeMode(string data)
    {
        // 数値のみであれば"Numeric"
        for (int i = 0; i < data.Length; i++)
        {
            if (!char.IsDigit(data[i]))
            {
                return "Byte";
            }
        }
        return "Numeric";

        // 英数字およびスペースのみであれば"Alphanumeric"
        for (int i = 0; i < data.Length; i++)
        {
            if (!char.IsLetterOrDigit(data[i]) && data[i] != ' ')
            {
                return "Byte";
            }
        }
        return "Alphanumeric";

        // それ以外は"Byte"
        return "Byte";
    }




    // バイトデータをエンコードするメソッド
    public byte[] EncodeByteData(string data)
    {
        // 各文字をASCIIコードとしてエンコード
        byte[] result = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            result[i] = (byte)data[i];
        }
        return result;
    }


    //
    // データのエンコードで使用されるメソッド群
    //
    // このメソッドは、与えられた数値データを3桁ごとに10ビットのバイナリ表現に変換し、
    // そのバイナリデータをバイト配列にエンコードする。
    public byte[] EncodeNumericData(string data)
    {
        int arraySize = ((data.Length + 2) / 3) * 10 / 8;
        if (((data.Length + 2) / 3) * 10 % 8 != 0) arraySize++;

        byte[] result = new byte[arraySize];
        int resultIndex = 0;

        for (int i = 0; i < data.Length; i += 3)
        {
            string segment = data.Substring(i, i + 3 > data.Length ? data.Length - i : 3);
            int number = int.Parse(segment);
            string binaryString = ToBinaryString(number, 10);

            for (int bit = 0; bit < 10; bit += 8)
            {
                string byteString = binaryString.Substring(bit, bit + 8 > binaryString.Length ? binaryString.Length - bit : 8);
                result[resultIndex++] = BinaryStringToByte(byteString);
            }
        }
        return result;
    }

    // 数値をバイナリ文字列に変換するヘルパーメソッド
    string ToBinaryString(int number, int bits)
    {
        string result = "";
        for (int i = bits - 1; i >= 0; i--)
        {
            result += (number & (1 << i)) != 0 ? "1" : "0";
        }
        return result;
    }
    // バイナリ文字列をバイトに変換するヘルパーメソッド
    byte BinaryStringToByte(string binaryString)
    {
        byte result = 0;
        for (int i = 0; i < binaryString.Length; i++)
        {
            if (binaryString[i] == '1')
            {
                result |= (byte)(1 << (binaryString.Length - 1 - i));
            }
        }
        return result;
    }

    // このメソッドは、与えられた英数字データを2文字ごとに11ビットのバイナリ表現に変換し、
    // そのバイナリデータをバイト配列にエンコードする。
    public byte[] EncodeAlphanumericData(string data)
    {
        // 最終的なバイト配列のサイズを計算する
        int bitsNeeded = (data.Length / 2) * 11;
        if (data.Length % 2 == 1) bitsNeeded += 6; // 奇数の場合は最後の文字に対して6ビット追加
        int bytesNeeded = (bitsNeeded + 7) / 8; // バイトに変換

        byte[] result = new byte[bytesNeeded];
        int bitIndex = 0;

        for (int i = 0; i < data.Length; i++)
        {
            int value = GetAlphanumericValue(data[i]);
            int bitsToWrite = (i % 2 == 0 && i < data.Length - 1) ? 11 : 6; // 2文字目または奇数の最後の文字で6ビット
            for (int bit = bitsToWrite - 1; bit >= 0; bit--)
            {
                int bitValue = (value >> bit) & 1;
                if (bitValue == 1)
                {
                    int byteIndex = bitIndex / 8;
                    int bitPosition = 7 - (bitIndex % 8); // バイト内のビット位置
                    result[byteIndex] |= (byte)(1 << bitPosition);
                }
                bitIndex++;
            }
        }

        return result;
    }

    // 英数字データに対応する値を返すメソッド
    private int GetAlphanumericValue(char c)
    {
        if (c >= '0' && c <= '9') return c - '0';
        if (c >= 'A' && c <= 'Z') return c - 'A' + 10;
        if (c == ' ') return 36;
        // サポートされていない文字の場合は-1を返す
        return -1;
    }

    // データバイト列を多項式形式に変換する
    private int[] PreparePolynomialData(byte[] data)
    {
        int[] polynomialData = new int[data.Length];

        // 各データバイトを多項式の項に変換
        for (int i = 0; i < data.Length; i++)
        {
            polynomialData[i] = data[i];
        }

        return polynomialData;
    }


    int DetermineErrorCorrectionCodeLength(ErrorCorrectionLevel level, int dataLength)
    {
        // QRコードのバージョン1におけるエラー訂正コードの長さを定義
        int length = 0;
        switch (level)
        {
            case ErrorCorrectionLevel.L:
                length = 7; // バージョン1の低エラー訂正レベルでのコードワード数
                break;
            case ErrorCorrectionLevel.M:
                length = 10; // バージョン1の中エラー訂正レベルでのコードワード数
                break;
            case ErrorCorrectionLevel.Q:
                length = 13; // バージョン1の高エラー訂正レベルでのコードワード数
                break;
            case ErrorCorrectionLevel.H:
                length = 17; // バージョン1の最高エラー訂正レベルでのコードワード数
                break;
        }
        return length;
    }


    public int[] CalculateGeneratorPolynomial(int errorCorrectionCodeLength) 
    {
        int[] generator = new int[1] {1}; // 最初の項は x^0 なので 1

        for (int i = 1; i <= errorCorrectionCodeLength; i++) {
            int alphaPower = antilogTable[i - 1]; // α^(i-1) で適切な冪を取得
            int[] term = new int[] {1, alphaPower};
            int[] newGenerator = new int[generator.Length + 1];

            for (int j = 0; j < generator.Length; j++) {
                newGenerator[j] = generator[j]; // 既存の項をコピー
            }

            for (int j = 0; j < term.Length; j++) {
                for (int k = 0; k < generator.Length; k++) {
                    newGenerator[k + j] ^= GaloisFieldMultiply(term[j], generator[k]);
                }
            }
            generator = newGenerator;
        }
        return generator;
    }



    // 多項式の乗算（ガロア体GF(2^8)を使用した実装）
    int[] MultiplyPolynomials(int[] a, int[] b)
    {
        // 結果の多項式配列を初期化します。長さは、入力された2つの多項式の長さの和から1を引いたものです。
        // これは、2つの多項式を乗算した結果の最大次数が、それぞれの多項式の次数の和に等zZしいからです。
        int[] result = new int[a.Length + b.Length - 1];
        
        // 外側のループは、最初の多項式の各係数に対して実行されます。
        for (int i = 0; i < a.Length; i++)
        {
            // 内側のループは、2番目の多項式の各係数に対して実行されます。
            for (int j = 0; j < b.Length; j++)
            {
                // ここで、a[i]とb[j]の乗算をガロア体GF(2^8)内で行います。
                // GaloisFieldMultiplyメソッドを使用して、2つの係数の乗算を実行し、
                // その結果をresult[i + j]にXOR演算で加算します。ガロア体の加算はビット単位のXORと等価です。
                // XORを使用する理由は、GF(2^8)での加算がキャリーを持たず、単純なビット単位のXORによって行われるからです。
                result[i + j] ^= GaloisFieldMultiply(a[i], b[j]); 
            }
        }
        
        // 最終的に、乗算された多項式の係数が格納された配列を返します。
        // この配列は、エラー訂正コードの生成などに使用されます。
        return result;
    }


    // ガロア体GF(2^8)のテーブル生成メソッド
    void GenerateGaloisFieldTables()
    {
        // ログテーブルとアンチログテーブルを初期化
        int alpha = 2; // ガロア体の原始元αを2とします。これは多項式の根として機能します。
        int current = 1; // αの0乗は常に1です。この値からスタートして、αの乗数を増やしていきます。

        // 255回の繰り返しで、各αの乗数に対する値を計算します。
        for (int i = 0; i < 255; i++) // GF(2^8)では、2^8 - 1 = 255までの値を扱います。
        {
            // アンチログテーブルには、αのi乗の結果を保存します。
            antilogTable[i] = current;

            // ログテーブルには、その値が何乗で得られるかを記録します。
            logTable[current] = i;

            // αの次の乗数を計算します。この計算はGF(2^8)で行われるため、特殊な乗算が必要です。
            current = GaloisFieldMultiply(current, alpha); // ここでの乗算はGF(2^8)における乗算を意味します。
        }
        Debug.Log("test");
    }


    // ガロア体GF(2^8)での乗算メソッド
    int GaloisFieldMultiply(int a, int b) 
    {
        if (a == 0 || b == 0) {
            return 0;  // ゼロに何を掛けても結果はゼロです。
        }

        int logA = logTable[a];
        int logB = logTable[b];
        int logResult = (logA + logB) % 255;  // 正しいモジュロ計算
        if (logResult == 255) logResult = 0;  // 255の場合、0にラップアラウンド

        return antilogTable[logResult];
    }



    //
    // 以下はApplyMaskPatternsの実装において必要なメソッド群
    //
    bool[][][] ApplyMaskPatterns(bool[][] qrCodeMatrix)
    {
        bool[][][] maskedMatrices = new bool[8][][];

        // 各マスクパターンの初期化
        for (int i = 0; i < 8; i++)
        {
            maskedMatrices[i] = new bool[qrCodeMatrix.Length][];
            for (int j = 0; j < qrCodeMatrix.Length; j++)
            {
                maskedMatrices[i][j] = (bool[])qrCodeMatrix[j].Clone();
            }
        }

        // 8種類のマスクパターンの適用
        for (int maskIndex = 0; maskIndex < 8; maskIndex++)
        {
            for (int row = 0; row < qrCodeMatrix.Length; row++)
            {
                for (int col = 0; col < qrCodeMatrix[row].Length; col++)
                {
                    // マスクパターンのルールに基づいてビットを反転させる
                    if (ShouldApplyMask(maskIndex, row, col))
                    {
                        maskedMatrices[maskIndex][row][col] = !maskedMatrices[maskIndex][row][col];
                    }
                }
            }
        }

        return maskedMatrices;
    }

    //
    // 以下はCalculatePenaltyScoresの実装において必要なメソッド群
    // 
    int[] CalculatePenaltyScores(bool[][][] maskedMatrices)
    {
        int[] penaltyScores = new int[8];

        for (int i = 0; i < maskedMatrices.Length; i++)
        {
            int penaltyScore = 0;

            // ペナルティルール1: 同じ色のセルが連続する場合のスコア
            penaltyScore += CalculatePenaltyRule1(maskedMatrices[i]);

            // ペナルティルール2: 2x2の同色ブロックが存在する場合のスコア
            penaltyScore += CalculatePenaltyRule2(maskedMatrices[i]);

            // ペナルティルール3: 特定のパターンが存在する場合のスコア
            penaltyScore += CalculatePenaltyRule3(maskedMatrices[i]);

            // ペナルティルール4: 全体の暗いセルと明るいセルの比率
            penaltyScore += CalculatePenaltyRule4(maskedMatrices[i]);

            penaltyScores[i] = penaltyScore;
        }

        return penaltyScores;
    }
    int CalculatePenaltyRule1(bool[][] matrix)
    {
        int penalty = 0;
        int size = matrix.Length;
        
        // 水平方向のチェック
        for (int y = 0; y < size; y++)
        {
            bool prevCell = matrix[y][0];
            int count = 1;
            for (int x = 1; x < size; x++)
            {
                if (matrix[y][x] == prevCell)
                {
                    count++;
                    if (count == 5) penalty += 3;
                    else if (count > 5) penalty++;
                }
                else
                {
                    count = 1;
                    prevCell = matrix[y][x];
                }
            }
        }

        // 垂直方向のチェック
        for (int x = 0; x < size; x++)
        {
            bool prevCell = matrix[0][x];
            int count = 1;
            for (int y = 1; y < size; y++)
            {
                if (matrix[y][x] == prevCell)
                {
                    count++;
                    if (count == 5) penalty += 3;
                    else if (count > 5) penalty++;
                }
                else
                {
                    count = 1;
                    prevCell = matrix[y][x];
                }
            }
        }
        
        return penalty;
    }
    int CalculatePenaltyRule2(bool[][] matrix)
    {
        int penalty = 0;
        int size = matrix.Length - 1;
        
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                bool cell = matrix[y][x];
                if (cell == matrix[y][x+1] && cell == matrix[y+1][x] && cell == matrix[y+1][x+1])
                {
                    penalty += 3;
                }
            }
        }
        
        return penalty;
    }
    int CalculatePenaltyRule3(bool[][] matrix) {
        int penalty = 0;
        // パターン "10111010000" とその逆
        bool[] pattern1 = new bool[] { true, false, true, true, true, false, true, false, false, false, false };
        bool[] pattern2 = new bool[] { false, false, false, false, true, false, true, true, true, false, true };

        int size = matrix.Length;

        // 水平方向のチェック
        for (int y = 0; y < size; y++) {
            penalty += CountPatternOccurrences(matrix[y], pattern1, pattern2);
        }

        // 垂直方向のチェック
        for (int x = 0; x < size; x++) {
            bool[] colArray = new bool[size];
            for (int y = 0; y < size; y++) {
                colArray[y] = matrix[y][x];
            }
            penalty += CountPatternOccurrences(colArray, pattern1, pattern2);
        }

        return penalty * 40; // 仕様に基づき、見つかったパターンごとに40ポイント加算
    }
    int CalculatePenaltyRule4(bool[][] matrix)
    {
        int penalty = 0; 
        int darkCells = 0;
        int totalCells = matrix.Length * matrix.Length;
        
        foreach (var row in matrix)
        {
            foreach (bool cell in row)
            {
                if (cell) darkCells++;
            }
        }
        
        int percentDark = (int)((double)darkCells / totalCells * 100);
        int previousMultipleOfFive = percentDark / 5 * 5;
        int nextMultipleOfFive = previousMultipleOfFive + 5;
        penalty = Mathf.Min(Mathf.Abs(previousMultipleOfFive - 50) / 5, Mathf.Abs(nextMultipleOfFive - 50) / 5) * 10;
        
        return penalty;
    }

    // このメソッドは、指定されたパターンが文字列にいくつ含まれているかを数える
    int CountPatternOccurrences(bool[] data, bool[] pattern1, bool[] pattern2) {
        int count = 0;
        int patternLength = pattern1.Length;

        for (int i = 0; i <= data.Length - patternLength; i++) {
            bool match1 = true;
            bool match2 = true;
            for (int j = 0; j < patternLength; j++) {
                if (data[i + j] != pattern1[j]) {
                    match1 = false;
                }
                if (data[i + j] != pattern2[j]) {
                    match2 = false;
                }
                if (!match1 && !match2) break;
            }
            if (match1 || match2) count++;
        }

        return count;
    }


    //
    // 以下はSelectBestMaskの実装に必要なメソッドです。
    //
    int SelectBestMask(int[] penaltyScores)
    {
        // 最小のペナルティスコアとそのインデックスを初期化
        int minScore = int.MaxValue;
        int bestMaskIndex = 0;

        // 各マスクパターンのペナルティスコアを走査
        for (int i = 0; i < penaltyScores.Length; i++)
        {
            // もし現在のスコアがこれまでの最小スコアより小さいなら、更新する
            if (penaltyScores[i] < minScore)
            {
                minScore = penaltyScores[i];
                bestMaskIndex = i;
            }
        }

        // 最適なマスクパターンのインデックスを返す
        return bestMaskIndex;
    }

    //
    // 以下はApplyBestMaskの実装に必要なメソッド群です。
    //
    bool[][] ApplyBestMask(bool[][] qrCodeMatrix, int bestMaskIndex)
    {
        int size = 21; // バージョン1のQRコードのサイズは21x21
        bool[][] finalMatrix = new bool[size][];

        // マトリックスの初期化
        for (int i = 0; i < size; i++)
        {
            finalMatrix[i] = new bool[size];
            for (int j = 0; j < size; j++)
            {
                finalMatrix[i][j] = qrCodeMatrix[i][j];
            }
        }

        // 選択されたマスクパターンを適用
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (ShouldApplyMask(bestMaskIndex, row, col) && !IsReserved(finalMatrix, row, col))
                {
                    // マスクパターンの条件に基づいてビットを反転
                    finalMatrix[row][col] = !finalMatrix[row][col];
                }
            }
        }

        return finalMatrix;
    }

    bool ShouldApplyMask(int maskIndex, int row, int col)
    {
        // ここに、マスクパターンのルールに従った条件分岐を実装
        switch (maskIndex)
        {
            case 0: return (row + col) % 2 == 0;
            case 1: return row % 2 == 0;
            case 2: return col % 3 == 0;
            case 3: return (row + col) % 3 == 0;
            case 4: return (row / 2 + col / 3) % 2 == 0;
            case 5: return ((row * col) % 2) + ((row * col) % 3) == 0;
            case 6: return (((row * col) % 2) + ((row * col) % 3)) % 2 == 0;
            case 7: return (((row + col) % 2) + ((row * col) % 3)) % 2 == 0;
            default: return false;
        }
    }

    //
    // 以降はガロア体において使用するメソッド群です
    //
    byte[] ConvertErrorCorrectionPolynomialToBytes(int[] errorCorrectionPolynomial)
    {
        byte[] errorCorrectionBytes = new byte[errorCorrectionPolynomial.Length];
        for (int i = 0; i < errorCorrectionPolynomial.Length; i++)
        {
            // ガロア体の値は0~255の範囲なので、byte型にキャスト可能
            errorCorrectionBytes[i] = (byte)errorCorrectionPolynomial[i];
        }
        return errorCorrectionBytes;
    }

    // 未実装
    // フォーマット情報の配置
    // エンコードされたフォーマット情報をQRコードマトリックスに配置する
    void PlaceFormatInformation(bool[][] qrCodeMatrix, byte[] formatInformation)
    {
        // フォーマット情報をファインダーパターンの近くの特定の位置に配置
        // 左上、右上、左下のファインダーパターンの外側に隣接する位置に配置
    }

    //
    // EncodeFormatInformationに必要なメソッド群
    //
    byte[] EncodeFormatInformation(ErrorCorrectionLevel errorCorrectionLevel, int maskPattern)
    {
        // ここでは、エラー訂正レベルとマスクパターンからフォーマット情報をビット列としてエンコードし、
        // それにBCHコードを使ってエラー訂正ビットを追加する処理を疑似的に実装する。

        // エラー訂正レベルとマスクパターンをビット列にエンコード
        // この部分は疑似コードであり、実際のエンコード処理はプロジェクトの要件に応じて異なる
        int formatInfo = (int)errorCorrectionLevel << 3 | maskPattern;

        // BCHコードを計算してエラー訂正ビットを追加（疑似コード）
        int bchCode = CalculateBCHCode(formatInfo); // BCHコードの計算メソッドは実装依存

        // エンコードされたフォーマット情報を15ビットのビット列として組み立てる
        int encodedFormatInfo = (formatInfo << 10) | bchCode;

        // 15ビットのフォーマット情報をバイト配列に変換（最初のバイトの上位5ビットは不使用）
        byte[] formatInfoBytes = new byte[2];
        formatInfoBytes[0] = (byte)((encodedFormatInfo >> 8) & 0xFF);
        formatInfoBytes[1] = (byte)(encodedFormatInfo & 0xFF);

        return formatInfoBytes;
    }

    //
    // BCHコード計算
    //
    int CalculateBCHCode(int formatInfo)
    {
        // フォーマット情報に対する初期ビット列を生成
        int initialBits = formatInfo;

        // BCHジェネレータ多項式 (QRコードの仕様に基づく)
        int generatorPolynomial = 0b10100110111; // 仮の値

        // 初期ビット列に対してジェネレータ多項式で割り算を行い、余りを計算
        int remainder = PolynomialDivision(initialBits, generatorPolynomial);

        // 得られた余りがBCHコード
        return remainder;
    }



    // 多項式の次数を取得
    private int GetDegree(int polynomial)
    {
        // 最上位ビットがセットされている位置を取得
        int degree = 0;
        while ((polynomial & 0x80000000) != 0)
        {
            degree++;
            polynomial <<= 1;
        }

        return degree;
    }

    // 多項式の除算
    int PolynomialDivision(int dividend, int divisor)
    {
        int degreeDifference = GetDegree(dividend) - GetDegree(divisor);

        // QRコードバージョン1では、最大次数は7なので、剰余演算を行う
        degreeDifference %= 8;

        while (degreeDifference >= 0)
        {
            dividend ^= (divisor << degreeDifference);
            degreeDifference = GetDegree(dividend) - GetDegree(divisor);
        }

        return dividend;
    }


    // ガロア体における指数計算の修正版
    private int Pow(int x, int y)
    {
        // オブジェクトからインスタンスを抽出
        Frame_text frame_Text = outo_logObject.GetComponent<Frame_text>();

        if (y == 0)
        {
            // 任意の数の0乗は1
            return 1;
        }
        else if (y == 1)
        {
            // 任意の数の1乗はその数自身
            return x;
        }

        // 計算結果がint型の範囲を超える可能性があるため、long型で計算を行う
        long result = 1;
        for (int i = 0; i < y; i++)
        {
            result *= x;
            // ガロア体の計算ではオーバーフローを気にする必要がないが、
            // 実際のアプリケーションでは結果がint型の範囲内に収まることを保証する必要がある
            if (result > int.MaxValue)
            {
                frame_Text.WriteStr("Pow関数の計算結果がint型の最大値を超えました。");
                Debug.Log("Pow関数の計算結果がint型の最大値を超えました。");
            }
        }

        return (int)result; // 最終的な計算結果をint型にキャストして返す
    }

    // private void InitializeGaloisFieldTables()
    // {
    //     int x = 1;
    //     for (int i = 0; i < 255; i++)
    //     {
    //         antilogTable[i] = x;
    //         logTable[x] = i;
    //         x = (x * 2) ^ ((x & 0x80) > 0 ? 0x1B : 0);
    //         x %= 256;
    //     }
    //     logTable[1] = 0;
    //     antilogTable[255] = antilogTable[0];
    // }




}