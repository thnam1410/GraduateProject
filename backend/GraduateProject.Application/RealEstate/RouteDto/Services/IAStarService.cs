namespace GraduateProject.Application.RealEstate.RouteDto.Services;

public interface IAStarService
{
    Task<List<AStarNodeBusStop>> StartAlgorithmsBusStopPaths(StopDto startPoint, StopDto endPoint, List<StopDto> stopDtos);


    Task<List<AStarNode>> StartAlgorithmsRoutePaths(List<VertexDto> vertices, List<EdgeDto> edges, Guid startNodeId, Guid targetNodeId);

}