# Escrutinador

Read metadata from your entities no matter how they were defined.

--------
## Metadata provider
Escrutinador use IMetadataProvider 's implementation to read the metadata of your entities.

Nowadays the only available metadata provider is DataAnnotationsMetadataProvider.

But the idea is have a lot of IMetadataProvider tha allow define metadata using constants, database, xml files, fluent apis, etc.

## Setup
    PM> Install-Package Escrutinador
    PM> Install-Package Escrutinador.Extensions.EntityFramework
    PM> Install-Package Escrutinador.Extensions.KissSpecifications

## Usage
### Reading metadata from DataAnnotation
For an entity, like MyEntity below:

```csharp
public class MyEntity
{
    [StringLength(50, MinimumLength = 10)]
    [Required]
    [Display(Order = 2)]
    public string Text { get; set; }
}
```

You can read the metadata in this way:
```csharp
var provider = EscrutinadorConfig.MetadataProvider;
var metadata = provider.Property<MyEntity>(p => p.Text);
Console.WriteLine("Name: {0}", metadata.Name);
Console.WriteLine("MinLength: {0}", metadata.MinLength);
Console.WriteLine("MaxLength: {0}", metadata.MaxLength);
Console.WriteLine("Order: {0}", metadata.Order);
Console.WriteLine("Required: {0}", metadata.Required);
```
The console output will be:
    Name: Text
    MinLength: 10
    MaxLength: 50
    Order: 2
    Required: true

## Extensions
Escrutinador's extensions allow to extend the library to combine it with others libraries.

### Escrutinador.Extensions.EntityFramework
It has an EntityTypeConfiguration called [MetadataEntityTypeConfiguration](src/Escrutinador.Extensions.EntityFramework/MetadataEntityTypeConfiguration.cs) that allows auto map the EF entity using the information from metadata.

```csharp
public class MyEntityMap : MetadataEntityTypeConfiguration<MyEntity>
{
    public MyEntityMap()
    {
        this.ToTable("MyEntity");		
        this.MapMetadata(t => t.Text);		
    }
}
```
The line *"this.MapMetadata(t => t.Text);"*	is equivalent to:
```csharp
this.Property(t => t.Text).HasMaxLength(50).IsRequired();
```
### Escrutinador.Extensions.KissSpecifications
This extension has a [SpecificationBase](http://github.com/giacomelli/KissSpecification) called [MustComplyWithMetadataSpecification](src/Escrutinador.Extensions.KissSpecifications/MustComplyWithMetadataSpecification.cs) that verify if the entity's state is complying with the metadata.

```csharp
var entity = new MyEntity() { Text = "A" };
SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy(
	entity, 
	new MustComplyWithMetadataSpecification<MyEntity>());
```  

The code above will throw a SpecificationNotSatisfiedException because the property Text with value "A" does not comply with MinLength metadata information.

## FAQ

#### Having troubles? 
 - Ask on [Stack Overflow](http://stackoverflow.com/search?q=Escrutinador)

## Roadmap

  - Add new IMetadataProvider's implementation
  - Add a fluent metadata definer.
 
--------

## How to improve it?
- Create a fork of [Escrutinador](https://github.com/giacomelli/Escrutinador/fork). 
- Did you change it? [Submit a pull request](https://github.com/giacomelli/Escrutinador/pull/new/master).


## License
Licensed under the The MIT License (MIT).
In others words, you can use this library for developement any kind of software: open source, commercial, proprietary and alien.
