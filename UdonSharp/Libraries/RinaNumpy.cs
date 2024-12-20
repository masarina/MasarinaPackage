
/// this file made by Masarina(Twitter_@Masarina002)
/// meamメソッドはMean_FloatArrayに名前を変更しました(2024-11-15)

using UdonSharp;
using UnityEngine;

public class RinaNumpy : UdonSharpBehaviour
{
    //
    public static float Sum_FloatArray(float[] x) {
        float sum = 0;
        for (int i = 0; i < x.Length; i++) {
            sum += x[i];
        }
        return sum;
    }
    
    //
    public static float[] Divide_FloatArray_Float(float[] x, float y) {
        float epsilon = 1e-6f; // ゼロ除算を避けるための小さな値
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] / (y + epsilon); // イプシロンを追加してゼロ除算を避ける
        }
        return result;
    }
    
    //
    public static float[] Subtract_FloatArray_Float(float[] x, float y) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] - y;
        }
        return result;
    }
    
    public static float[] Multiply_FloatArray_FloatArray(float[] x, float[] y) {
        if (x.Length != y.Length) {
            Debug.LogError("Arrays must be of equal length.");
            return new float[0]; // 空の配列を返すことで処理を安全に終了
        }
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] * y[i];
        }
        return result;
    }

    
    //
    public static float[] Multiply_FloatArray_Float(float[] x, float y) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] * y;
        }
        return result;
    }
    
    //
    public static float[] Add_FloatArray_FloatArray(float[] x, float[] y) {
        if (x.Length != y.Length) {
            Debug.LogError("Arrays must be of equal length.");
            return new float[0]; // 空の配列を返す
        }
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] + y[i];
        }
        return result;
    }

    
    //
    public static float[] OnesLike_FloatArray(float[] x) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = 1;
        }
        return result;
    }
    
    //
    public static float[] ZerosLike_FloatArray(float[] x) {
        return new float[x.Length]; // Default initialization to 0
    }
    

    //
    public static float[] Negative_FloatArray(float[] x) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = -x[i];
        }
        return result;
    }
    
    //
    public static float DotProduct_FloatArray_FloatArray(float[] x, float[] y) {
        if (x.Length != y.Length) {
            Debug.LogError("Arrays must be of equal length for dot product.");
            return 0; // エラー時は0を返す
        }
        float result = 0;
        for (int i = 0; i < x.Length; i++) {
            result += x[i] * y[i];
        }
        return result;
    }

    //
    public static float[] DotProduct_FloatArray2D_FloatArray(float[][] A, float[] x) {
        if (A.Length == 0 || A[0].Length != x.Length) {
            Debug.LogError("Matrix columns and vector size must match for dot product.");
            return new float[0];
        }
    
        float[] result = new float[A.Length];
        for (int i = 0; i < A.Length; i++) {
            for (int j = 0; j < x.Length; j++) {
                result[i] += A[i][j] * x[j];
            }
        }
        return result;
    }
    
    //
        public static float Mean_FloatArray(float[] x) 
    {
        float sum = 0;
        for (int i = 0; i < x.Length; i++) 
        {
            sum += x[i];
        }
        return sum / x.Length;
    }

    

    
    public static float[] Power_FloatArray_Float(float[] x, float y) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = Mathf.Pow(x[i], y);
        }
        return result;
    }
    
    public static float[] Sum_FloatArray2d_Float_axis0(float[][] x)
    {
        float[] sum = new float[x[0].Length];
        for (int i = 0; i < x.Length; i++)
        {
            for (int j = 0; j < x[i].Length; j++)
            {
                sum[j] += x[i][j];
            }
        }
        return sum;
    }
    
    public static float[] Sum_FloatArray2d_FloatArray2d_axis1(float[][] x)
    {
        float[] sum = new float[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            for (int j = 0; j < x[i].Length; j++)
            {
                sum[i] += x[i][j];
            }
        }
        return sum;
    }
    
    // 分散を計算するためのヘルパーメソッド
    public float Var_FloatArray(float[] x, float mean)
    {
        float sumOfSquares = 0f;
        for (int i = 0; i < x.Length; i++) 
        {
            sumOfSquares += Mathf.Pow(x[i] - mean, 2);
        }
        return sumOfSquares / x.Length;
    }

    
    /// <summary>
    /// 整数を指定したビット数の2進数文字列に変換します。
    /// </summary>
    /// <param name="value">変換する整数値</param>
    /// <param name="bitCount">指定するビット数</param>
    /// <returns>指定ビット数の2進数文字列</returns>
    public string ConvertToBinary(int value, int bitCount)
    {
        string binaryString = "";  // 変換結果の2進数文字列を保持する変数
        for (int i = 0; i < bitCount; i++)
        {
            // 指定のビット位置の値を取得し、文字列に追加
            binaryString = ((value >> i) & 1) + binaryString;
        }

        // ビット数に満たない場合、左側に0を追加して補完
        while (binaryString.Length < bitCount)
        {
            binaryString = "0" + binaryString;
        }

        return binaryString;
    }
    
    public static void CopyIntArray(int[] source, int[] destination, int length)
    {
        // 指定された長さ分だけsourceからdestinationへコピーします
        for (int i = 0; i < length; i++)
        {
            destination[i] = source[i];
        }
    }

    public int BitStringToInt(string bitString)
    {
        int result = 0;
        for (int i = 0; i < bitString.Length; i++)
        {
            if (bitString[i] == '1')
            {
                result += (1 << (bitString.Length - i - 1));  // ビットシフトを使って各桁を加算
            }
        }
        return result;
    }

    public static float Std_FloatArray(float[] x) {
        float mean = Mean_FloatArray(x);
        float sumOfSquares = 0f;
        for (int i = 0; i < x.Length; i++) {
            sumOfSquares += Mathf.Pow(x[i] - mean, 2);
        }
        return Mathf.Sqrt(sumOfSquares / x.Length);
    }



    // 配列を文字列に変換するメソッド
    private string IntArrayToString(int[] array)
    {
        // 配列を文字列に変換
        int[] tempArray = new int[array.Length]; // コピー用の一時配列
        this.CopyIntArray(array, tempArray, array.Length); // 配列をコピー
        string result = "";

        for (int i = 0; i < tempArray.Length; i++)
        {
            result += tempArray[i].ToString(); // 各要素を文字列化して結合
            if (i < tempArray.Length - 1)
            {
                result += ", "; // 区切り文字を追加
            }
        }

        return result;
    }

    public static string IntToStr(int value) {
        string result = "";
        bool isNegative = value < 0;
    
        if (isNegative) {
            value = -value; // 負の値を扱うため正の値に変換
        }
    
        do {
            int digit = value % 10; // 1桁ずつ取り出す
            result = (char)('0' + digit) + result; // 文字に変換して前に追加
            value /= 10;
        } while (value > 0);
    
        if (isNegative) {
            result = "-" + result; // 元が負ならマイナス符号を追加
        }
    
        return result;
    }

    public static int StrToInt(string value) {
        int result = 0;
        bool isNegative = false;
        int startIndex = 0;
    
        if (value.Length > 0 && value[0] == '-') {
            isNegative = true;
            startIndex = 1; // マイナス符号がある場合は1文字目から処理を始める
        }
    
        for (int i = startIndex; i < value.Length; i++) {
            char c = value[i];
            if (c < '0' || c > '9') {
                Debug.LogError($"Invalid character '{c}' in input string: {value}");
                return 0; // 無効な文字が含まれている場合は0を返す
            }
            result = result * 10 + (c - '0'); // 数値として計算
        }
    
        return isNegative ? -result : result; // 符号を適用して返す
    }

    public static float[][] CreateArray2d(int[] shape)
    {
        if (shape.Length != 2)
        {
            Debug.LogError("Shape must be a 2-element array [rows, columns].");
            return new float[0][]; // 空の配列を返す
        }
    
        int rows = shape[0];
        int columns = shape[1];
    
        // ジャグ配列を初期化
        float[][] jaggedArray = new float[rows][];
        for (int i = 0; i < rows; i++)
        {
            jaggedArray[i] = new float[columns];
        }
    
        return jaggedArray;
    }

    public static float[][][] CreateArray3d(int[] shape)
    {
        if (shape.Length != 3)
        {
            Debug.LogError("Shape must be a 3-element array [depth, rows, columns].");
            return new float[0][][]; // 空の配列を返す
        }
    
        int depth = shape[0];
        int rows = shape[1];
        int columns = shape[2];
    
        // 3次元配列を初期化
        float[][][] array3d = new float[depth][][];
        for (int d = 0; d < depth; d++)
        {
            array3d[d] = new float[rows][];
            for (int r = 0; r < rows; r++)
            {
                array3d[d][r] = new float[columns];
            }
        }
    
        return array3d;
    }

    public static float[][][][] CreateArray4d(int[] shape)
    {
        if (shape.Length != 4)
        {
            Debug.LogError("Shape must be a 4-element array [depth, height, rows, columns].");
            return new float[0][][][]; // 空の配列を返す
        }
    
        int depth = shape[0];
        int height = shape[1];
        int rows = shape[2];
        int columns = shape[3];
    
        // 4次元配列を初期化
        float[][][][] array4d = new float[depth][][][];
        for (int d = 0; d < depth; d++)
        {
            array4d[d] = new float[height][][];
            for (int h = 0; h < height; h++)
            {
                array4d[d][h] = new float[rows][];
                for (int r = 0; r < rows; r++)
                {
                    array4d[d][h][r] = new float[columns];
                }
            }
        }
    
        return array4d;
    }

    public static float[] Append_FloatArray(float[] array, float value)
    {
        // 新しいサイズの配列を作成
        float[] result = new float[array.Length + 1];
    
        // 元の配列をコピー
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = array[i];
        }
    
        // 新しい値を最後に追加
        result[array.Length] = value;
    
        return result;
    }

    public static int[] Append_IntArray(int[] array, int value)
    {
        // 新しいサイズの配列を作成
        int[] result = new int[array.Length + 1];
    
        // 元の配列をコピー
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = array[i];
        }
    
        // 新しい値を最後に追加
        result[array.Length] = value;
    
        return result;
    }

    public static string[] Append_StringArray(string[] array, string value)
    {
        // 新しいサイズの配列を作成
        string[] result = new string[array.Length + 1];
    
        // 元の配列をコピー
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = array[i];
        }
    
        // 新しい値を最後に追加
        result[array.Length] = value;
    
        return result;
    }

    public static float[][] AppendRow_FloatArray2D(float[][] matrix, float[] newRow)
    {
        // 元の行列の行数を取得
        int originalRows = matrix.Length;
    
        // 新しい行列を作成(行数を1つ増やす)
        float[][] newMatrix = new float[originalRows + 1][];
    
        // 元の行列を新しい行列にコピー
        for (int i = 0; i < originalRows; i++)
        {
            newMatrix[i] = matrix[i];
        }
    
        // 新しい行を最後に追加
        newMatrix[originalRows] = newRow;
    
        return newMatrix;
    }

    // 1次元ジャグ配列を加算するメソッド
    public float[][] AddFloatArray2dFloatArray2d(float[][] array1, float[][] array2)
    {
        // 配列がnullの場合のチェック
        if (array1 == null || array2 == null)
        {
            Debug.LogError("One or both of the input arrays are null.");
            return null;
        }

        // 配列の長さが一致するかチェック
        if (array1.Length != array2.Length)
        {
            Debug.LogError("Arrays must have the same number of elements.");
            return null;
        }

        // 結果を格納する新しい配列
        float[][] result = new float[array1.Length][];

        // 各要素を加算
        for (int i = 0; i < array1.Length; i++)
        {
            // 内部配列の長さが一致するかチェック
            if (array1[i].Length != array2[i].Length)
            {
                Debug.LogError($"Sub-array lengths do not match at index {i}.");
                return null;
            }

            // 各要素を加算
            result[i] = new float[array1[i].Length];
            for (int j = 0; j < array1[i].Length; j++)
            {
                result[i][j] = array1[i][j] + array2[i][j];
            }
        }

        return result;
    }

    public static float[] AddFloatArrayFloatArray(float[] array1, float[] array2)
    {
        // 配列がnullの場合のチェック
        if (array1 == null || array2 == null)
        {
            Debug.LogError("One or both of the input arrays are null.");
            return null;
        }
    
        // 配列の長さが一致するかチェック
        if (array1.Length != array2.Length)
        {
            Debug.LogError("Arrays must have the same length.");
            return null;
        }
    
        // 結果を格納する新しい配列
        float[] result = new float[array1.Length];
    
        // 各要素を加算
        for (int i = 0; i < array1.Length; i++)
        {
            result[i] = array1[i] + array2[i];
        }
    
        return result;
    }
            
}
