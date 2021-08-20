# CuteS 

### Example program in CuteS
```C#
namespace HelloWorld
{
    class Example
    {
        let bool: Flag = true;
        
        let int: MagicNumber = 1337;
        
        fn int: Square(int: x)
        {
            return x * x;
        }    
    }
}
```

### This is the same program compiled in C#
```C#
namespace HelloWorld
{
    public class Example
    {
        public bool Flag = true;
        
        public int MagicNumber = 1337;
    
        public int Square(int x)
        {
            return x * x;
        }
    }
}
```

## Features of CuteS
- #### Every classes are public 
```C#
// CuteS                                       // C#
class PublicClass { }                          public class PublicClass { } 
```

- #### Every fields and methods in classes are public
```C#
// CuteS                                       // C#
class PublicClass                              public class PublicClass
{                                              {
    let int: Field = 1;                             public int Field = 1;
    
    let string: Str = "a";                          public string Str = "a";
    
    fn void: F() { return; }                        public void F() { return; }
}                                              }
```
