//GeneticAlgorithm
#if defined(TORQUE2D_DEBUG) || defined(TORQUE2D_RELEASE)

ConsoleMethod( GeneticAlgorithm, run, string, 3, 3, "(string) Add the object to a scene.\n"
                                                      "@param string the filepath you need to read from."
                                                      "@return string The chromosome.")
{
    return object->run();
}

#endif