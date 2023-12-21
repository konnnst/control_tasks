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

    private string GetStaticModifier(FieldInfo entity)
    {
        if (entity.IsStatic)
            return "static ";
        else return "";
    }
    private List<string> ReadClassFields(Type someClass)
    {
        var fields = new List<string>();

        foreach (var field in someClass.GetFields())
        {
            var accessModifier = GetAccessModifier(field);
            var staticModifier = GetStaticModifier(field);
            fields.Add($"{accessModifier}{staticModifier}{field.GetType().Name }{field.Name};");
        }
        return fields;
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
    
    private string GetStaticModifier(MethodInfo entity)
    {
        if (entity.IsStatic)
            return "static ";
        else return "";
    }
    private string GetParameters(MethodInfo entity)
    {
        var parameters = new List<string>();

        foreach (var parameter in entity.GetParameters())
        {
            parameters.Add($"{parameter.GetType().Name} {parameter.Name}");
        }

        return String.Join(", ", parameters);
    }
    private List<string> ReadClassMethods(Type someClass)
    {
        var methods = new List<string>();

        foreach (var method in someClass.GetMethods())
        {
            var accessModifier = GetAccessModifier(method);
            var staticModifier = GetStaticModifier(method);
            methods.Add($"{accessModifier}{staticModifier}{method.Name}({GetParameters(method)}) default({method.ReturnType})");
        }
        return methods;
    }

    private List<List<string>> ReadClassSubclasses(Type someClass)
    {
        var subclasses = new List<List<string>>();

        return subclasses;
    }

    public List<string> ReadClass(Type someClass)
    {
        var result = new List<string>();
        var className = someClass.Name;
        var fields = ReadClassFields(someClass);
        var classes = ReadClassSubclasses(someClass);
        var methods = ReadClassMethods(someClass);

        result.Add($"class {className}\n{{");
        foreach (var field in fields)
        {
            result.Add(field);
        }
        foreach (var method in methods)
        {
            result.Add(method);
        }
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

    }
}