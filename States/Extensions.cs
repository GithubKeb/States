namespace Stateful;

public static class Extensions
{
	public static IServiceCollection AddState<T>(this IServiceCollection services
												,Func<T> factory = null)
	{
		if (factory == null)
		{
			services.AddScoped<State<T>>();
		}
		else
		{
			var val = factory();
			var state = new State<T>();
			state.Set(val);
			services.AddScoped<State<T>>(sp => state);
		}

		services.AddScoped<IReadOnlyState<T>>(sp => sp.GetRequiredService<State<T>>());
		services.AddScoped<IStateSetter<T>>(sp => sp.GetRequiredService<State<T>>());
		return services;
	}
	
	public static IServiceCollection AddModifiableState<T>(this IServiceCollection services
												,Func<T> factory = null)
	{
		if (factory == null)
		{
			services.AddScoped<ModifiableState<T>>();
		}
		else
		{
			var val = factory();
			services.AddScoped<ModifiableState<T>>(sp => { 
				var state = new ModifiableState<T>();
				state.Set(val);
				return state;
			});
		}

		services.AddScoped<IReadOnlyState<T>>(sp => sp.GetRequiredService<ModifiableState<T>>());
		services.AddScoped<IStateSetter<T>>(sp => sp.GetRequiredService<ModifiableState<T>>());
		return services;
	}
}