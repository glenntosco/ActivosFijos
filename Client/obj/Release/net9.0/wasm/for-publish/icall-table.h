#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
222,
233,
234,
235,
236,
237,
238,
239,
240,
241,
244,
245,
365,
366,
367,
395,
396,
397,
424,
425,
426,
543,
544,
545,
548,
586,
587,
588,
591,
593,
595,
597,
602,
610,
611,
612,
613,
614,
615,
616,
617,
618,
619,
620,
621,
622,
623,
624,
625,
626,
628,
629,
630,
631,
632,
633,
634,
728,
729,
730,
731,
732,
733,
734,
735,
736,
737,
738,
739,
740,
741,
742,
743,
744,
746,
747,
748,
749,
750,
751,
752,
814,
823,
824,
895,
902,
905,
907,
912,
913,
915,
916,
920,
921,
923,
924,
927,
928,
929,
932,
934,
937,
939,
941,
950,
1018,
1020,
1022,
1032,
1033,
1034,
1036,
1042,
1043,
1044,
1045,
1046,
1054,
1055,
1056,
1060,
1061,
1063,
1065,
1283,
1471,
1472,
9394,
9395,
9397,
9398,
9399,
9400,
9401,
9403,
9404,
9405,
9406,
9424,
9426,
9431,
9433,
9435,
9437,
9488,
9489,
9491,
9492,
9493,
9494,
9495,
9497,
9499,
10526,
10530,
10532,
10533,
10534,
10535,
10979,
10980,
10981,
10982,
11000,
11001,
11002,
11047,
11129,
11132,
11140,
11141,
11142,
11143,
11144,
11487,
11492,
11493,
11524,
11568,
11575,
11582,
11593,
11597,
11622,
11704,
11706,
11716,
11718,
11719,
11720,
11727,
11742,
11762,
11763,
11771,
11773,
11780,
11781,
11784,
11786,
11791,
11797,
11798,
11805,
11807,
11819,
11822,
11823,
11824,
11835,
11845,
11851,
11852,
11853,
11855,
11856,
11873,
11875,
11890,
11910,
11911,
11936,
11941,
11971,
11972,
12530,
12551,
12639,
12640,
12907,
12908,
12916,
12917,
12918,
12924,
12994,
13555,
13556,
14027,
14028,
14029,
14034,
14044,
14917,
14938,
14940,
14942,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
int ves_icall_System_GC_GetCollectionCount (int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Acos (double);
double ves_icall_System_Math_Acosh (double);
double ves_icall_System_Math_Asin (double);
double ves_icall_System_Math_Asinh (double);
double ves_icall_System_Math_Atan (double);
double ves_icall_System_Math_Atan2 (double,double);
double ves_icall_System_Math_Atanh (double);
double ves_icall_System_Math_Cbrt (double);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Cosh (double);
double ves_icall_System_Math_Exp (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sinh (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_Tanh (double);
double ves_icall_System_Math_FusedMultiplyAdd (double,double,double);
double ves_icall_System_Math_Log2 (double);
double ves_icall_System_Math_ModF (double,int);
float ves_icall_System_MathF_Acos (float);
float ves_icall_System_MathF_Acosh (float);
float ves_icall_System_MathF_Asin (float);
float ves_icall_System_MathF_Asinh (float);
float ves_icall_System_MathF_Atan (float);
float ves_icall_System_MathF_Atan2 (float,float);
float ves_icall_System_MathF_Atanh (float);
float ves_icall_System_MathF_Cbrt (float);
float ves_icall_System_MathF_Ceiling (float);
float ves_icall_System_MathF_Cos (float);
float ves_icall_System_MathF_Cosh (float);
float ves_icall_System_MathF_Exp (float);
float ves_icall_System_MathF_Floor (float);
float ves_icall_System_MathF_Log (float);
float ves_icall_System_MathF_Log10 (float);
float ves_icall_System_MathF_Pow (float,float);
float ves_icall_System_MathF_Sin (float);
float ves_icall_System_MathF_Sinh (float);
float ves_icall_System_MathF_Sqrt (float);
float ves_icall_System_MathF_Tan (float);
float ves_icall_System_MathF_Tanh (float);
float ves_icall_System_MathF_FusedMultiplyAdd (float,float,float);
float ves_icall_System_MathF_Log2 (float);
float ves_icall_System_MathF_ModF (float,int);
int ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw (int,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
int64_t ves_icall_System_Threading_Interlocked_Add_Long (int,int64_t);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw (int,int,int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
int ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_CustomAttributeBuilder_GetBlob_raw (int,int,int,int,int,int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_Debugger_IsAttached_internal ();
int ves_icall_System_Diagnostics_Debugger_IsLogging ();
void ves_icall_System_Diagnostics_Debugger_Log (int,int,int);
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 222,
ves_icall_System_Array_InternalCreate,
// token 233,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 234,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 235,
ves_icall_System_Array_CanChangePrimitive,
// token 236,
ves_icall_System_Array_FastCopy,
// token 237,
ves_icall_System_Array_GetLengthInternal_raw,
// token 238,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 239,
ves_icall_System_Array_GetGenericValue_icall,
// token 240,
ves_icall_System_Array_GetValueImpl_raw,
// token 241,
ves_icall_System_Array_SetGenericValue_icall,
// token 244,
ves_icall_System_Array_SetValueImpl_raw,
// token 245,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 365,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 366,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 367,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 395,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 396,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 397,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 424,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 425,
ves_icall_System_Enum_InternalGetCorElementType,
// token 426,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 543,
ves_icall_System_Environment_get_ProcessorCount,
// token 544,
ves_icall_System_Environment_get_TickCount,
// token 545,
ves_icall_System_Environment_get_TickCount64,
// token 548,
ves_icall_System_Environment_FailFast_raw,
// token 586,
ves_icall_System_GC_GetCollectionCount,
// token 587,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 588,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 591,
ves_icall_System_GC_SuppressFinalize_raw,
// token 593,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 595,
ves_icall_System_GC_GetGCMemoryInfo,
// token 597,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 602,
ves_icall_System_Object_MemberwiseClone_raw,
// token 610,
ves_icall_System_Math_Acos,
// token 611,
ves_icall_System_Math_Acosh,
// token 612,
ves_icall_System_Math_Asin,
// token 613,
ves_icall_System_Math_Asinh,
// token 614,
ves_icall_System_Math_Atan,
// token 615,
ves_icall_System_Math_Atan2,
// token 616,
ves_icall_System_Math_Atanh,
// token 617,
ves_icall_System_Math_Cbrt,
// token 618,
ves_icall_System_Math_Ceiling,
// token 619,
ves_icall_System_Math_Cos,
// token 620,
ves_icall_System_Math_Cosh,
// token 621,
ves_icall_System_Math_Exp,
// token 622,
ves_icall_System_Math_Floor,
// token 623,
ves_icall_System_Math_Log,
// token 624,
ves_icall_System_Math_Log10,
// token 625,
ves_icall_System_Math_Pow,
// token 626,
ves_icall_System_Math_Sin,
// token 628,
ves_icall_System_Math_Sinh,
// token 629,
ves_icall_System_Math_Sqrt,
// token 630,
ves_icall_System_Math_Tan,
// token 631,
ves_icall_System_Math_Tanh,
// token 632,
ves_icall_System_Math_FusedMultiplyAdd,
// token 633,
ves_icall_System_Math_Log2,
// token 634,
ves_icall_System_Math_ModF,
// token 728,
ves_icall_System_MathF_Acos,
// token 729,
ves_icall_System_MathF_Acosh,
// token 730,
ves_icall_System_MathF_Asin,
// token 731,
ves_icall_System_MathF_Asinh,
// token 732,
ves_icall_System_MathF_Atan,
// token 733,
ves_icall_System_MathF_Atan2,
// token 734,
ves_icall_System_MathF_Atanh,
// token 735,
ves_icall_System_MathF_Cbrt,
// token 736,
ves_icall_System_MathF_Ceiling,
// token 737,
ves_icall_System_MathF_Cos,
// token 738,
ves_icall_System_MathF_Cosh,
// token 739,
ves_icall_System_MathF_Exp,
// token 740,
ves_icall_System_MathF_Floor,
// token 741,
ves_icall_System_MathF_Log,
// token 742,
ves_icall_System_MathF_Log10,
// token 743,
ves_icall_System_MathF_Pow,
// token 744,
ves_icall_System_MathF_Sin,
// token 746,
ves_icall_System_MathF_Sinh,
// token 747,
ves_icall_System_MathF_Sqrt,
// token 748,
ves_icall_System_MathF_Tan,
// token 749,
ves_icall_System_MathF_Tanh,
// token 750,
ves_icall_System_MathF_FusedMultiplyAdd,
// token 751,
ves_icall_System_MathF_Log2,
// token 752,
ves_icall_System_MathF_ModF,
// token 814,
ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw,
// token 823,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 824,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 895,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 902,
ves_icall_RuntimeType_make_array_type_raw,
// token 905,
ves_icall_RuntimeType_make_byref_type_raw,
// token 907,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 912,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 913,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 915,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 916,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 920,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 921,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 923,
ves_icall_System_RuntimeType_getFullName_raw,
// token 924,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 927,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 928,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 929,
ves_icall_RuntimeType_GetFields_native_raw,
// token 932,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 934,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 937,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 939,
ves_icall_RuntimeType_GetName_raw,
// token 941,
ves_icall_RuntimeType_GetNamespace_raw,
// token 950,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 1018,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 1020,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 1022,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 1032,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 1033,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 1034,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 1036,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 1042,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 1043,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 1044,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 1045,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 1046,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 1054,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 1055,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 1056,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 1060,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 1061,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 1063,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 1065,
ves_icall_System_String_FastAllocateString_raw,
// token 1283,
ves_icall_System_Type_internal_from_handle_raw,
// token 1471,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1472,
ves_icall_System_ValueType_Equals_raw,
// token 9394,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 9395,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 9397,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 9398,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 9399,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 9400,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 9401,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 9403,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 9404,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 9405,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 9406,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 9424,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 9426,
mono_monitor_exit_icall_raw,
// token 9431,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 9433,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 9435,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 9437,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 9488,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 9489,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 9491,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 9492,
ves_icall_System_Threading_Thread_GetState_raw,
// token 9493,
ves_icall_System_Threading_Thread_SetState_raw,
// token 9494,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 9495,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 9497,
ves_icall_System_Threading_Thread_YieldInternal,
// token 9499,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 10526,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 10530,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 10532,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 10533,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 10534,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 10535,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 10979,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 10980,
ves_icall_System_GCHandle_InternalFree_raw,
// token 10981,
ves_icall_System_GCHandle_InternalGet_raw,
// token 10982,
ves_icall_System_GCHandle_InternalSet_raw,
// token 11000,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 11001,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 11002,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 11047,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 11129,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 11132,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 11140,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 11141,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 11142,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 11143,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 11144,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw,
// token 11487,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 11492,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 11493,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 11524,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 11568,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 11575,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 11582,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 11593,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 11597,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 11622,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 11704,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 11706,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 11716,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 11718,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 11719,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 11720,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 11727,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 11742,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 11762,
ves_icall_reflection_get_token_raw,
// token 11763,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 11771,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 11773,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 11780,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 11781,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 11784,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 11786,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 11791,
ves_icall_reflection_get_token_raw,
// token 11797,
ves_icall_get_method_info_raw,
// token 11798,
ves_icall_get_method_attributes,
// token 11805,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 11807,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 11819,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 11822,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 11823,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 11824,
ves_icall_reflection_get_token_raw,
// token 11835,
ves_icall_InternalInvoke_raw,
// token 11845,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 11851,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 11852,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 11853,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 11855,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 11856,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 11873,
ves_icall_InvokeClassConstructor_raw,
// token 11875,
ves_icall_InternalInvoke_raw,
// token 11890,
ves_icall_reflection_get_token_raw,
// token 11910,
ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw,
// token 11911,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 11936,
ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw,
// token 11941,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 11971,
ves_icall_reflection_get_token_raw,
// token 11972,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 12530,
ves_icall_CustomAttributeBuilder_GetBlob_raw,
// token 12551,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 12639,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 12640,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 12907,
ves_icall_ModuleBuilder_basic_init_raw,
// token 12908,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 12916,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 12917,
ves_icall_ModuleBuilder_getToken_raw,
// token 12918,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 12924,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 12994,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 13555,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 13556,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 14027,
ves_icall_System_Diagnostics_Debugger_IsAttached_internal,
// token 14028,
ves_icall_System_Diagnostics_Debugger_IsLogging,
// token 14029,
ves_icall_System_Diagnostics_Debugger_Log,
// token 14034,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 14044,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 14917,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 14938,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 14940,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 14942,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
0,
0,
0,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
0,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
};
