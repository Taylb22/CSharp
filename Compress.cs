using System;

void printBinary (byte[] vetor) {
    foreach (byte val in vetor) {
        Console.Write(Convert.ToString(val, 2).PadLeft(8, '0'));
        Console.Write(' ');
        Console.WriteLine(val);
    }
}

byte[] compact (byte[] originalData) { 
    byte[] result;
    if ((byte)(originalData.Length & 1) == 1) {
        int len = (originalData.Length / 2) + 1;
        result = new byte[len];
        result[len-1] = (byte)(originalData[originalData.Length-1] >> 4);
    } else {
        result = new byte[originalData.Length / 2];
    }

    int j = 0;
    for (int i = 0; i < (originalData.Length / 2); i++) {
        byte val = (byte)(originalData[j] >> 4);
        val = (byte)((byte)(val << 4) + (byte)(originalData[j+1] >> 4));
        result[i] = val;
        j = j + 2;
    }
    return result;
}

byte[] decompact(byte[] compactedData, int fillingMode = 0) {
    int fill;
    switch (fillingMode) {
        case 0:
            fill = 0b0000;
            break;
        case 1:
            fill = 0b1111;
            break;
        case 2:
            fill = 0b0111;
            break;
        default:
            return [];
    }

    byte[] result;
    int length;
    int iterate;
    if ((byte)(compactedData[compactedData.Length-1] >> 4) == 0) {
        length = (compactedData.Length * 2) - 1;
        iterate = compactedData.Length - 1;
        result = new byte[length];
        result[length-1] = (byte)((byte)(compactedData[compactedData.Length-1] << 4) | fill);
    } else {
        length = compactedData.Length * 2;
        iterate = compactedData.Length;
        result = new byte[length];
    }

    int j = 0;
    for (int i = 0; i < iterate; i++) {
        byte first = (byte)((byte)((byte)(compactedData[i] >> 4) << 4) | fill);
        byte second = (byte)((byte)(compactedData[i] << 4) | fill);
        result[j] = first;
        result[j+1] = second;
        j = j + 2;
    }
    return result;
}

byte[] vetor = {0b11111111, 0b10101111, 0b01000001, 0b10101111};
Console.WriteLine("Before Compression:");
printBinary(vetor);
Console.WriteLine();

byte[] compressed = compact(vetor);
Console.WriteLine("After Compression:");
printBinary(compressed);
Console.WriteLine();

byte[] decompressed = decompact(compressed, fillingMode: 2);
Console.WriteLine("After Decompression:");
printBinary(decompressed);
Console.WriteLine();