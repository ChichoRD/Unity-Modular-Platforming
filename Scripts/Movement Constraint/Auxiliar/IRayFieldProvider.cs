using System.Collections.Generic;

public interface IRayFieldProvider
{
    IEnumerable<SizedRay> GetRayField();
}
