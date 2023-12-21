namespace Reflector;
using System.Reflection;

public class Reflector
{
    private string _save_directory;
    public Reflector()
    {
        _save_directory = Directory.GetCurrentDirectory() + "/ReflectedClasses";
    }
    public Reflector(string save_directory)
    {
        _save_directory = save_directory;
    }
    private string GetAccessModifier(FieldInfo entity)  
    {
        if (entity.IsPrivate)
            return "private ";
        else if (entity.IsFamily)
            return "protected ";
        else if (entity.IsAssembly)
            return "internal ";
        else if (entity.IsFamilyOrAssembly)
            return "protected internal ";
        else if (entity.IsPublic)
            return "public ";
        throw new ArgumentException("Access modifier is not correst");
    }
    private string GetAccessModifier(MethodInfo entity)
    {
        if (entity.IsPrivate)
            return "private ";
        else if (entity.IsFamily)
            return "protected ";
        else if (entity.IsAssembly)
            return "internal ";
        else if (entity.IsFamilyOrAssembly)
            return "protected internal ";
        else if (entity.IsPublic)
            return "public ";
        throw new ArgumentException("Access modifier is not correst");
    }
    private string GetAccessModifier(Type entity)
    {
        if (entity.IsPublic)
            return "public ";
        else
            return "private ";
    }
    private string GetStaticModifier(MethodInfo entity)
    {
        if (entity.IsStatic)
            return "static ";
        else return "";
    }
    private string GetStaticModifier(Type entity)
    {
        if (entity.IsAbstract && entity.IsSealed)
            return "static ";
        else return "";
    }
    private string GetStaticModifier(FieldInfo entity)
    {
        if (entity.IsStatic)
            return "static ";
        else return "";
    }
    private List<string> ReadFields(Type someClass)
    {
        var fields = new List<string>();

        foreach (var field in someClass.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var accessModifier = GetAccessModifier(field);
            var staticModifier = GetStaticModifier(field);
            fields.Add($"{accessModifier}{staticModifier}{field.FieldType} {field.Name};");
        }
        fields.Sort();
        return fields;
    }
    private string GetParameters(MethodInfo entity)
    {
        var parameters = new List<string>();

        foreach (var parameter in entity.GetParameters())
        {
            parameters.Add($"{parameter.ParameterType.Name} {parameter.Name}");
        }
        
        return String.Join(", ", parameters);
    }
    private List<string> ReadMethods(Type someClass)
    {
        var methods = new List<string>();
        foreach (var method in someClass.GetMethods())
        {
            var accessModifier = GetAccessModifier(method);
            var staticModifier = GetStaticModifier(method);
            methods.Add($"{accessModifier}{staticModifier}{method.ReturnType.Name} {method.Name}({GetParameters(method)}) {{ return default({method.ReturnType}); }}");
        }
        methods.Sort();
        return methods;
    }
    private string GetParameters(ConstructorInfo entity)
    {
        var parameters = new List<string>();

        foreach (var parameter in entity.GetParameters())
        {
            var p = parameter.GetType().GetType().Name;
            Console.WriteLine(p);
            parameters.Add($"{parameter.ParameterType} {parameter.Name}");
        }

        return String.Join(", ", parameters);
    }
    private List<string> ReadConstructors(Type someClass)
    {
        var constructors = new List<string>();

        foreach (var constructor in someClass.GetConstructors())
        {
            constructors.Add($"public {someClass.Name}({GetParameters(constructor)}) {{ }}");
        }
        constructors.Sort();
        return constructors;
    }
    private List<List<string>> ReadSubclasses(Type someClass)
    {
        var subclasses = new List<List<string>>();
        foreach (var subclass in someClass.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
        {
            subclasses.Add(ReadClass(subclass));
        }
        return subclasses;
    }
    private List<string> ReadClass(Type someClass)
    {
        var result = new List<string>();
        var className = someClass.Name;
        var fields = ReadFields(someClass);
        var subclasses = ReadSubclasses(someClass);
        var methods = ReadMethods(someClass);
        var constructors = ReadConstructors(someClass); 
        var accessModifier = GetAccessModifier(someClass);
        var staticModifier = GetStaticModifier(someClass);

        result.Add($"{accessModifier}{staticModifier}class {className}\n{{");
        foreach (var subclass in subclasses)
        {
            foreach (var line in subclass)
                result.Add(line);
        }
        foreach (var field in fields)
            result.Add(field);
        foreach (var constructor in constructors)
            result.Add(constructor);
        foreach (var method in methods)
            result.Add(method);
        result.Add("}");
        
        return result;
    }
    public void PrintStructure(Type someClass)
    {
        var className = someClass.Name;
        using (var writer = new StreamWriter($"{_save_directory}/{className}.cs"))
        {
            var classInfo = ReadClass(someClass);
            foreach (var line in classInfo)
            {
                writer.WriteLine(line);
            }
        }
    }
    public void DiffClasses(Type a, Type b)
    {
        var fieldsA = ReadFields(a);
        var fieldsB = ReadFields(b);
        var methodsA = ReadMethods(a);
        var methodsB = ReadMethods(b);
        var A = a.Name;
        var B = b.Name;

        Console.WriteLine($"Fields {A} / Fields {B}");
        foreach (var field in fieldsA)
        {
            if (!fieldsB.Contains(field))
                Console.WriteLine(field);
        }

        Console.WriteLine($"Fields {B} / Fields {A}");
        foreach (var field in fieldsB)
        {
            if (!fieldsA.Contains(field))
                Console.WriteLine(field);
        }

        Console.WriteLine($"Methods {A} / Methods {B}");
        foreach (var method in methodsA)
        {
            if (!methodsB.Contains(method))
                Console.WriteLine(method);
        }

        Console.WriteLine($"Methods {B} / Methods {A}");
        foreach (var method in methodsB)
        {
            if (!methodsA.Contains(method))
                Console.WriteLine(method);
        }
    }
}