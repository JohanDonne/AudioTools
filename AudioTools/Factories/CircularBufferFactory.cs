using AudioTools.Implementation;
using AudioTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTools.Factories;
public class CircularBufferFactory : ICircularBufferFactory
{
    public ICircularBuffer<T> Create<T>(int capacity)
    {
        return new CircularBuffer<T>(capacity);
    }
}
