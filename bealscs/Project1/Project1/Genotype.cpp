#include "Genotype.h"

using namespace DarwinCharles;


	Genotype::Genotype()
	{
		fitness = 0;
		rFitness = 0;
		cFitness = 0;
	}
			
	vector<int> Genotype::spliceGene (int *, int *)
	{

	}

	void Genotype::genePushBack( int g )
	{
		gene.push_back( g );
		return;
	}

	vector<int> Genotype::getGene()			//return a pointer?
	{
		return gene;
	}
			
	double Genotype::getFitness()
	{
		return fitness;
	}
			
	double Genotype::getRFitness()
	{
		return rFitness;
	}
			
	double Genotype::getCFitness()
	{
		return cFitness;
	}

	void Genotype::setGene()
	{
		return;
	}

	void Genotype::setFitness(double f)
	{
		fitness = f;
	}
			
	void Genotype::setRFitness(double r)
	{
		rFitness = r;
	}
			
	void Genotype::setCFitness(double c)
	{
		cFitness = c;
	}