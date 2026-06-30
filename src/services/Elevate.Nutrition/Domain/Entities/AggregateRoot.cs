using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Domain.Entities;

public abstract class AggregateRoot<TId> : BaseEntity<TId> where TId : notnull { }
