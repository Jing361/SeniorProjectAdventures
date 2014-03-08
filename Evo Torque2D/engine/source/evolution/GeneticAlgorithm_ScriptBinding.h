/*//GeneticAlgorithm
#if defined(TORQUE2D_RELEASE)
#include "GeneticAlgorithm.h"
#include "sim/simBase.h"

ConsoleMethod(GeneticAlgorithm, run, const char *, 3, 3, "() Gets the object's position.\n"
                                                              "@return chromosome.")
{
	// Fetch result.  
    string result = object->run(argv[2]);  
    // Create Returnable Buffer.  
	char* pBuffer = Con::getReturnBuffer(result.size()*sizeof(string));  

	dSprintf(pBuffer, result.size()*sizeof(string), "%s", result);  

	return pBuffer; 
}

#endif*/