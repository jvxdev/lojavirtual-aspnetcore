namespace LojaVirtual.Libraries.AutoMapper
{
    public static class ExtensionBase
    {
        public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source) => Mapper.Map(source, destination);
    }
}