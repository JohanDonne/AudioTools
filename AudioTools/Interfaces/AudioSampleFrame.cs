namespace AudioTools.Interfaces;

public struct AudioSampleFrame : IEquatable<AudioSampleFrame>
{
    public float Left { get; }
    public float Right { get; }

    public AudioSampleFrame(float left, float right)
    {
        Left = left < -1F ? -1F : left > 1F ? 1F : left;
        Right = right < -1F ? -1F : right > 1F ? 1F : right;
    }

    public static bool operator ==(AudioSampleFrame a, AudioSampleFrame b)
    {
        return a.Left == b.Left && a.Right == b.Right;
    }

    public static bool operator !=(AudioSampleFrame a, AudioSampleFrame b)
    {
        return !(a == b);
    }

    public static AudioSampleFrame operator +(AudioSampleFrame a, AudioSampleFrame b)
    {
        return new AudioSampleFrame(
                                left: SaturatedAdd(a.Left, b.Left),
                                right: SaturatedAdd(a.Right, b.Right));
    }

    public static AudioSampleFrame operator -(AudioSampleFrame a, AudioSampleFrame b)
    {
        return new AudioSampleFrame(
                                left: SaturatedSubtract(a.Left, b.Left),
                                right: SaturatedSubtract(a.Right, b.Right));
    }

    public static AudioSampleFrame operator -(AudioSampleFrame a)
    {
        return new AudioSampleFrame(left: -a.Left, right: -a.Right);
    }

    public AudioSampleFrame Amplify(float factor)
    {
        return new AudioSampleFrame(
                                left: SaturatedMultiply(Left, factor),
                                right: SaturatedMultiply(Right, factor));
    }

    public bool Equals(AudioSampleFrame other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return Left.GetHashCode() ^ Right.GetHashCode();
    }

    private static float SaturatedAdd(float a, float b)
    {
        float l = a + b;
        return l < -1 ? -1F : l > 1 ? 1F : l;
    }

    private static float SaturatedSubtract(float a, float b)
    {
        float l = a - b;
        return l < -1 ? -1F : l > 1 ? 1F : l;
    }

    private static float SaturatedMultiply(float a, float b)
    {
        float l = a * b;
        return l < -1 ? -1F : l > 1 ? 1F : l;
    }

    public override bool Equals(object? obj)
    {
        return obj is AudioSampleFrame frame && Equals(frame);
    }

    public override string ToString()
    {
        return $"Left: {Left}, Right: {Right}";
    }
}
